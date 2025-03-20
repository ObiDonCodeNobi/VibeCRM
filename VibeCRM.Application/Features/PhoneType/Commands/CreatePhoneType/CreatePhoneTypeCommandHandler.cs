using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.PhoneType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PhoneType.Commands.CreatePhoneType
{
    /// <summary>
    /// Handler for the CreatePhoneTypeCommand.
    /// Creates a new phone type in the database.
    /// </summary>
    public class CreatePhoneTypeCommandHandler : IRequestHandler<CreatePhoneTypeCommand, PhoneTypeDto>
    {
        private readonly IPhoneTypeRepository _phoneTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePhoneTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePhoneTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="phoneTypeRepository">The phone type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public CreatePhoneTypeCommandHandler(
            IPhoneTypeRepository phoneTypeRepository,
            IMapper mapper,
            ILogger<CreatePhoneTypeCommandHandler> logger)
        {
            _phoneTypeRepository = phoneTypeRepository ?? throw new ArgumentNullException(nameof(phoneTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreatePhoneTypeCommand by creating a new phone type.
        /// </summary>
        /// <param name="request">The command containing the phone type details to create.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created phone type DTO.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the creation process.</exception>
        public async Task<PhoneTypeDto> Handle(CreatePhoneTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating new phone type with type: {PhoneType}", request.Type);

                // Map command to entity
                var phoneTypeEntity = _mapper.Map<Domain.Entities.TypeStatusEntities.PhoneType>(request);

                // Set audit fields
                phoneTypeEntity.Id = Guid.NewGuid();
                phoneTypeEntity.CreatedDate = DateTime.UtcNow;
                phoneTypeEntity.CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"); // This should be replaced with the actual user ID
                phoneTypeEntity.ModifiedDate = DateTime.UtcNow;
                phoneTypeEntity.ModifiedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"); // This should be replaced with the actual user ID
                phoneTypeEntity.Active = true;

                // Add to repository
                var createdPhoneType = await _phoneTypeRepository.AddAsync(phoneTypeEntity, cancellationToken);

                // Map to DTO
                var phoneTypeDto = _mapper.Map<PhoneTypeDto>(createdPhoneType);

                _logger.LogInformation("Successfully created phone type with ID: {PhoneTypeId}", phoneTypeDto.Id);

                return phoneTypeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating phone type: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}

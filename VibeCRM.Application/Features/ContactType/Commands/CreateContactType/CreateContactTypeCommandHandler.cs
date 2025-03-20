using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.ContactType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ContactType.Commands.CreateContactType
{
    /// <summary>
    /// Handler for the CreateContactTypeCommand.
    /// Creates a new contact type in the database.
    /// </summary>
    public class CreateContactTypeCommandHandler : IRequestHandler<CreateContactTypeCommand, ContactTypeDto>
    {
        private readonly IContactTypeRepository _contactTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateContactTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateContactTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="contactTypeRepository">The contact type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public CreateContactTypeCommandHandler(
            IContactTypeRepository contactTypeRepository,
            IMapper mapper,
            ILogger<CreateContactTypeCommandHandler> logger)
        {
            _contactTypeRepository = contactTypeRepository ?? throw new ArgumentNullException(nameof(contactTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateContactTypeCommand by creating a new contact type.
        /// </summary>
        /// <param name="request">The command containing the contact type details to create.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created contact type DTO.</returns>
        public async Task<ContactTypeDto> Handle(CreateContactTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating new contact type with type: {ContactType}", request.Type);

                // Map command to entity
                var contactTypeEntity = _mapper.Map<Domain.Entities.TypeStatusEntities.ContactType>(request);

                // Set audit fields
                contactTypeEntity.Id = Guid.NewGuid();
                contactTypeEntity.CreatedDate = DateTime.UtcNow;
                contactTypeEntity.CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"); // This should be replaced with the actual user ID
                contactTypeEntity.ModifiedDate = DateTime.UtcNow;
                contactTypeEntity.ModifiedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"); // This should be replaced with the actual user ID
                contactTypeEntity.Active = true;

                // Add to repository
                var createdContactType = await _contactTypeRepository.AddAsync(contactTypeEntity, cancellationToken);

                // Map to DTO
                var contactTypeDto = _mapper.Map<ContactTypeDto>(createdContactType);

                _logger.LogInformation("Successfully created contact type with ID: {ContactTypeId}", contactTypeDto.Id);

                return contactTypeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating contact type: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
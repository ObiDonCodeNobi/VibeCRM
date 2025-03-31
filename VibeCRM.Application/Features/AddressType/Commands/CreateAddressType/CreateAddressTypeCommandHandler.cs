using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.AddressType;

namespace VibeCRM.Application.Features.AddressType.Commands.CreateAddressType
{
    /// <summary>
    /// Handler for the CreateAddressTypeCommand.
    /// Creates a new address type in the database.
    /// </summary>
    public class CreateAddressTypeCommandHandler : IRequestHandler<CreateAddressTypeCommand, AddressTypeDto>
    {
        private readonly IAddressTypeRepository _addressTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateAddressTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAddressTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="addressTypeRepository">The address type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public CreateAddressTypeCommandHandler(
            IAddressTypeRepository addressTypeRepository,
            IMapper mapper,
            ILogger<CreateAddressTypeCommandHandler> logger)
        {
            _addressTypeRepository = addressTypeRepository ?? throw new ArgumentNullException(nameof(addressTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateAddressTypeCommand by creating a new address type.
        /// </summary>
        /// <param name="request">The command containing the address type details to create.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created address type DTO.</returns>
        public async Task<AddressTypeDto> Handle(CreateAddressTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating new address type with type: {AddressType}", request.Type);

                // Map command to entity
                var addressTypeEntity = _mapper.Map<Domain.Entities.TypeStatusEntities.AddressType>(request);

                // Set audit fields
                addressTypeEntity.Id = Guid.NewGuid();
                addressTypeEntity.CreatedDate = DateTime.UtcNow;
                addressTypeEntity.CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"); // This should be replaced with the actual user ID
                addressTypeEntity.ModifiedDate = DateTime.UtcNow;
                addressTypeEntity.ModifiedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"); // This should be replaced with the actual user ID
                addressTypeEntity.Active = true;

                // Add to repository
                var createdAddressType = await _addressTypeRepository.AddAsync(addressTypeEntity, cancellationToken);

                // Map to DTO
                var addressTypeDto = _mapper.Map<AddressTypeDto>(createdAddressType);

                _logger.LogInformation("Successfully created address type with ID: {AddressTypeId}", addressTypeDto.Id);

                return addressTypeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating address type: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
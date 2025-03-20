using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.AddressType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.AddressType.Commands.UpdateAddressType
{
    /// <summary>
    /// Handler for the UpdateAddressTypeCommand.
    /// Updates an existing address type in the database.
    /// </summary>
    public class UpdateAddressTypeCommandHandler : IRequestHandler<UpdateAddressTypeCommand, AddressTypeDto>
    {
        private readonly IAddressTypeRepository _addressTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAddressTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAddressTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="addressTypeRepository">The address type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public UpdateAddressTypeCommandHandler(
            IAddressTypeRepository addressTypeRepository,
            IMapper mapper,
            ILogger<UpdateAddressTypeCommandHandler> logger)
        {
            _addressTypeRepository = addressTypeRepository ?? throw new ArgumentNullException(nameof(addressTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateAddressTypeCommand by updating an existing address type.
        /// </summary>
        /// <param name="request">The command containing the address type details to update.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated address type DTO.</returns>
        public async Task<AddressTypeDto> Handle(UpdateAddressTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating address type with ID: {AddressTypeId}", request.Id);

                // Get existing entity
                var existingAddressType = await _addressTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingAddressType == null)
                {
                    _logger.LogWarning("Address type with ID {AddressTypeId} not found", request.Id);
                    throw new Exception($"Address type with ID {request.Id} not found");
                }

                // Update properties
                existingAddressType.Type = request.Type;
                existingAddressType.Description = request.Description;
                existingAddressType.OrdinalPosition = request.OrdinalPosition;
                existingAddressType.ModifiedDate = DateTime.UtcNow;
                existingAddressType.ModifiedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"); // This should be replaced with the actual user ID

                // Update in repository
                var updatedAddressType = await _addressTypeRepository.UpdateAsync(existingAddressType, cancellationToken);

                // Map to DTO
                var addressTypeDto = _mapper.Map<AddressTypeDto>(updatedAddressType);

                _logger.LogInformation("Successfully updated address type with ID: {AddressTypeId}", addressTypeDto.Id);

                return addressTypeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating address type with ID {AddressTypeId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
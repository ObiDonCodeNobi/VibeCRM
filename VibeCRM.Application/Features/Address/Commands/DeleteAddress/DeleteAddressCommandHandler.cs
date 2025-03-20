using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Address.Commands.DeleteAddress
{
    /// <summary>
    /// Handler for processing DeleteAddressCommand requests.
    /// Implements the CQRS command handler pattern for soft-deleting address entities.
    /// </summary>
    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, bool>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<DeleteAddressCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAddressCommandHandler"/> class.
        /// </summary>
        /// <param name="addressRepository">The address repository for database operations.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public DeleteAddressCommandHandler(
            IAddressRepository addressRepository,
            ILogger<DeleteAddressCommandHandler> logger)
        {
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteAddressCommand by soft-deleting an address entity in the database.
        /// Sets the Active property to false rather than physically removing the record.
        /// </summary>
        /// <param name="request">The DeleteAddressCommand containing the address ID to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>True if the address was successfully soft-deleted, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the address ID is empty.</exception>
        public async Task<bool> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.AddressId == Guid.Empty) throw new ArgumentException("Address ID cannot be empty", nameof(request.AddressId));
            if (request.ModifiedBy == Guid.Empty) throw new ArgumentException("Modified by user ID cannot be empty", nameof(request.ModifiedBy));

            try
            {
                // Get the existing address
                var existingAddress = await _addressRepository.GetByIdAsync(request.AddressId, cancellationToken);
                if (existingAddress == null)
                {
                    _logger.LogWarning("Address with ID {AddressId} not found or is already inactive", request.AddressId);
                    return false;
                }

                // Update the ModifiedBy property before deletion
                existingAddress.ModifiedBy = request.ModifiedBy;
                existingAddress.ModifiedDate = DateTime.UtcNow;

                // First update the address with the modified information
                await _addressRepository.UpdateAsync(existingAddress, cancellationToken);

                // Then soft delete the address (sets Active = false)
                var result = await _addressRepository.DeleteAsync(request.AddressId, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Soft-deleted address with ID: {AddressId}", request.AddressId);
                }
                else
                {
                    _logger.LogWarning("Failed to soft-delete address with ID: {AddressId}", request.AddressId);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while soft-deleting address with ID: {AddressId}", request.AddressId);
                throw;
            }
        }
    }
}
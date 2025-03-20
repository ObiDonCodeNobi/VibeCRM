using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.AddressType.Commands.DeleteAddressType
{
    /// <summary>
    /// Handler for the DeleteAddressTypeCommand.
    /// Performs a soft delete on an address type by setting Active = false.
    /// </summary>
    public class DeleteAddressTypeCommandHandler : IRequestHandler<DeleteAddressTypeCommand, bool>
    {
        private readonly IAddressTypeRepository _addressTypeRepository;
        private readonly ILogger<DeleteAddressTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAddressTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="addressTypeRepository">The address type repository.</param>
        /// <param name="logger">The logger.</param>
        public DeleteAddressTypeCommandHandler(
            IAddressTypeRepository addressTypeRepository,
            ILogger<DeleteAddressTypeCommandHandler> logger)
        {
            _addressTypeRepository = addressTypeRepository ?? throw new ArgumentNullException(nameof(addressTypeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteAddressTypeCommand by performing a soft delete on an address type.
        /// </summary>
        /// <param name="request">The command containing the ID of the address type to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the address type was successfully deleted; otherwise, false.</returns>
        public async Task<bool> Handle(DeleteAddressTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Soft deleting address type with ID: {AddressTypeId}", request.Id);

                // Check if address type exists
                var addressType = await _addressTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (addressType == null)
                {
                    _logger.LogWarning("Address type with ID {AddressTypeId} not found", request.Id);
                    return false;
                }

                // Perform soft delete by setting Active = false
                var result = await _addressTypeRepository.DeleteAsync(request.Id, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Successfully soft deleted address type with ID: {AddressTypeId}", request.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to soft delete address type with ID: {AddressTypeId}", request.Id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error soft deleting address type with ID {AddressTypeId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ShipMethod.Commands.DeleteShipMethod
{
    /// <summary>
    /// Handler for the DeleteShipMethodCommand.
    /// Handles the deletion of an existing shipping method.
    /// </summary>
    public class DeleteShipMethodCommandHandler : IRequestHandler<DeleteShipMethodCommand, bool>
    {
        private readonly IShipMethodRepository _shipMethodRepository;
        private readonly ILogger<DeleteShipMethodCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteShipMethodCommandHandler"/> class.
        /// </summary>
        /// <param name="shipMethodRepository">The ship method repository.</param>
        /// <param name="logger">The logger.</param>
        public DeleteShipMethodCommandHandler(
            IShipMethodRepository shipMethodRepository,
            ILogger<DeleteShipMethodCommandHandler> logger)
        {
            _shipMethodRepository = shipMethodRepository ?? throw new ArgumentNullException(nameof(shipMethodRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteShipMethodCommand by soft-deleting an existing shipping method.
        /// </summary>
        /// <param name="request">The command for deleting an existing shipping method.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the shipping method was successfully deleted; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the deletion process.</exception>
        public async Task<bool> Handle(DeleteShipMethodCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Deleting shipping method with ID: {ShipMethodId}", request.Id);

                // Check if the shipping method exists
                var existingShipMethod = await _shipMethodRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingShipMethod == null)
                {
                    _logger.LogWarning("Shipping method with ID: {ShipMethodId} not found", request.Id);
                    return false;
                }

                // Delete from repository (soft delete)
                var result = await _shipMethodRepository.DeleteAsync(request.Id, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Successfully deleted shipping method with ID: {ShipMethodId}", request.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to delete shipping method with ID: {ShipMethodId}", request.Id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting shipping method with ID {ShipMethodId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
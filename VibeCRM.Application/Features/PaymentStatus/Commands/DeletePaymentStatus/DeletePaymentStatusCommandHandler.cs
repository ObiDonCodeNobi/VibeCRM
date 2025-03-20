using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PaymentStatus.Commands.DeletePaymentStatus
{
    /// <summary>
    /// Handler for processing DeletePaymentStatusCommand requests.
    /// Implements the CQRS command handler pattern for soft deleting payment status entities.
    /// </summary>
    public class DeletePaymentStatusCommandHandler : IRequestHandler<DeletePaymentStatusCommand, bool>
    {
        private readonly IPaymentStatusRepository _paymentStatusRepository;
        private readonly ILogger<DeletePaymentStatusCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeletePaymentStatusCommandHandler"/> class.
        /// </summary>
        /// <param name="paymentStatusRepository">The payment status repository for database operations.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public DeletePaymentStatusCommandHandler(
            IPaymentStatusRepository paymentStatusRepository,
            ILogger<DeletePaymentStatusCommandHandler> logger)
        {
            _paymentStatusRepository = paymentStatusRepository ?? throw new ArgumentNullException(nameof(paymentStatusRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeletePaymentStatusCommand by soft deleting an existing payment status entity in the database.
        /// </summary>
        /// <param name="request">The DeletePaymentStatusCommand containing the ID of the payment status to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>True if the payment status was successfully deleted; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<bool> Handle(DeletePaymentStatusCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Deleting payment status with ID: {PaymentStatusId}", request.Id);

                // Check if the payment status exists
                var existingPaymentStatus = await _paymentStatusRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingPaymentStatus == null)
                {
                    _logger.LogWarning("Payment status with ID {PaymentStatusId} not found", request.Id);
                    return false;
                }

                // Perform soft delete
                await _paymentStatusRepository.DeleteAsync(request.Id, cancellationToken);

                _logger.LogInformation("Successfully deleted payment status with ID: {PaymentStatusId}", request.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting payment status with ID {PaymentStatusId}: {ErrorMessage}",
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}

using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Payment.Commands.DeletePayment
{
    /// <summary>
    /// Handler for processing DeletePaymentCommand requests.
    /// Implements the CQRS command handler pattern for soft-deleting payment records.
    /// </summary>
    public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, bool>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<DeletePaymentCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeletePaymentCommandHandler"/> class.
        /// </summary>
        /// <param name="paymentRepository">The payment repository for database operations.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public DeletePaymentCommandHandler(
            IPaymentRepository paymentRepository,
            ILogger<DeletePaymentCommandHandler> logger)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeletePaymentCommand by soft-deleting a payment record in the database.
        /// </summary>
        /// <param name="request">The DeletePaymentCommand containing the payment ID to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when validation fails or the payment is not found.</exception>
        public async Task<bool> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                // Get the existing payment
                var existingPayment = await _paymentRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingPayment == null)
                {
                    _logger.LogWarning("Payment with ID {PaymentId} not found for deletion", request.Id);
                    throw new ArgumentException($"Payment with ID {request.Id} not found", nameof(request.Id));
                }

                // Update modified by and modified date before deletion
                existingPayment.ModifiedBy = request.ModifiedBy;
                existingPayment.ModifiedDate = DateTime.UtcNow;

                // Update the payment to set the modified values
                await _paymentRepository.UpdateAsync(existingPayment, cancellationToken);

                // Soft delete the payment (sets Active = false) by ID
                bool result = await _paymentRepository.DeleteAsync(request.Id, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Payment with ID {PaymentId} was successfully soft-deleted", request.Id);
                }
                else
                {
                    _logger.LogWarning("Payment with ID {PaymentId} could not be soft-deleted", request.Id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error soft-deleting payment with ID {PaymentId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
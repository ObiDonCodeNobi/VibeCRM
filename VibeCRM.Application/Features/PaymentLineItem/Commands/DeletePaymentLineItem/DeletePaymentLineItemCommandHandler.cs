using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.PaymentLineItem.Commands.DeletePaymentLineItem
{
    /// <summary>
    /// Handler for processing DeletePaymentLineItemCommand requests.
    /// </summary>
    /// <remarks>
    /// Implements the CQRS command handler pattern for soft-deleting payment line item records.
    /// </remarks>
    public class DeletePaymentLineItemCommandHandler : IRequestHandler<DeletePaymentLineItemCommand, bool>
    {
        private readonly IPaymentLineItemRepository _paymentLineItemRepository;
        private readonly ILogger<DeletePaymentLineItemCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeletePaymentLineItemCommandHandler"/> class.
        /// </summary>
        /// <param name="paymentLineItemRepository">The payment line item repository for database operations.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public DeletePaymentLineItemCommandHandler(
            IPaymentLineItemRepository paymentLineItemRepository,
            ILogger<DeletePaymentLineItemCommandHandler> logger)
        {
            _paymentLineItemRepository = paymentLineItemRepository ?? throw new ArgumentNullException(nameof(paymentLineItemRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeletePaymentLineItemCommand by soft-deleting a payment line item record in the database.
        /// </summary>
        /// <param name="request">The DeletePaymentLineItemCommand containing the payment line item ID to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when validation fails or the payment line item is not found.</exception>
        public async Task<bool> Handle(DeletePaymentLineItemCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                // Get the existing payment line item
                var existingPaymentLineItem = await _paymentLineItemRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingPaymentLineItem == null)
                {
                    _logger.LogWarning("Payment line item with ID {PaymentLineItemId} not found for deletion", request.Id);
                    throw new ArgumentException($"Payment line item with ID {request.Id} not found", nameof(request.Id));
                }

                // Update modified by and modified date before deletion
                existingPaymentLineItem.ModifiedBy = Guid.TryParse(request.ModifiedBy, out var modifiedByGuid) ? modifiedByGuid : Guid.Empty;
                existingPaymentLineItem.ModifiedDate = DateTime.UtcNow;

                // Update the payment line item to set the modified values
                await _paymentLineItemRepository.UpdateAsync(existingPaymentLineItem, cancellationToken);

                // Soft delete the payment line item (sets Active = false) by ID
                bool result = await _paymentLineItemRepository.DeleteAsync(request.Id, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Payment line item with ID {PaymentLineItemId} was successfully soft-deleted", request.Id);
                }
                else
                {
                    _logger.LogWarning("Payment line item with ID {PaymentLineItemId} could not be soft-deleted", request.Id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error soft-deleting payment line item with ID {PaymentLineItemId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
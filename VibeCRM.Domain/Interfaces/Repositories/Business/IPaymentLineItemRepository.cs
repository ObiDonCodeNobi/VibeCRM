using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Business
{
    /// <summary>
    /// Repository interface for managing PaymentLineItem entities
    /// </summary>
    public interface IPaymentLineItemRepository : IRepository<PaymentLineItem, Guid>
    {
        /// <summary>
        /// Gets payment line items for a specific payment
        /// </summary>
        /// <param name="paymentId">The unique identifier of the payment</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payment line items associated with the specified payment</returns>
        Task<IEnumerable<PaymentLineItem>> GetByPaymentIdAsync(Guid paymentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets payment line items for a specific invoice
        /// </summary>
        /// <param name="invoiceId">The unique identifier of the invoice</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payment line items associated with the specified invoice</returns>
        Task<IEnumerable<PaymentLineItem>> GetByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets payment line items for a specific invoice line item
        /// </summary>
        /// <param name="invoiceLineItemId">The unique identifier of the invoice line item</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of payment line items associated with the specified invoice line item</returns>
        Task<IEnumerable<PaymentLineItem>> GetByInvoiceLineItemIdAsync(Guid invoiceLineItemId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the total payment amount for a specific invoice
        /// </summary>
        /// <param name="invoiceId">The unique identifier of the invoice</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The total amount paid for the specified invoice</returns>
        Task<decimal> GetTotalPaidForInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default);
    }
}
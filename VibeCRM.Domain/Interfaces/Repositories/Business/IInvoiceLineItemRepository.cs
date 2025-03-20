using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Business
{
    /// <summary>
    /// Repository interface for managing InvoiceLineItem entities
    /// </summary>
    public interface IInvoiceLineItemRepository : IRepository<InvoiceLineItem, Guid>
    {
        /// <summary>
        /// Gets invoice line items for a specific invoice
        /// </summary>
        /// <param name="invoiceId">The unique identifier of the invoice</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoice line items associated with the specified invoice</returns>
        Task<IEnumerable<InvoiceLineItem>> GetByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets invoice line items for a specific product
        /// </summary>
        /// <param name="productId">The unique identifier of the product</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoice line items associated with the specified product</returns>
        Task<IEnumerable<InvoiceLineItem>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets invoice line items for a specific service
        /// </summary>
        /// <param name="serviceId">The unique identifier of the service</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoice line items associated with the specified service</returns>
        Task<IEnumerable<InvoiceLineItem>> GetByServiceIdAsync(Guid serviceId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the total amount for a specific invoice
        /// </summary>
        /// <param name="invoiceId">The unique identifier of the invoice</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The total amount for the specified invoice</returns>
        Task<decimal> GetTotalForInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets invoice line items by date range
        /// </summary>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoice line items within the specified date range</returns>
        Task<IEnumerable<InvoiceLineItem>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    }
}
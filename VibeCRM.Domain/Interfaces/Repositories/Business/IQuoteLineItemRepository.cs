using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Business
{
    /// <summary>
    /// Repository interface for managing QuoteLineItem entities
    /// </summary>
    public interface IQuoteLineItemRepository : IRepository<QuoteLineItem, Guid>
    {
        /// <summary>
        /// Gets quote line items for a specific quote
        /// </summary>
        /// <param name="quoteId">The unique identifier of the quote</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quote line items associated with the specified quote</returns>
        Task<IEnumerable<QuoteLineItem>> GetByQuoteIdAsync(Guid quoteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets quote line items for a specific product
        /// </summary>
        /// <param name="productId">The unique identifier of the product</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quote line items associated with the specified product</returns>
        Task<IEnumerable<QuoteLineItem>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets quote line items for a specific service
        /// </summary>
        /// <param name="serviceId">The unique identifier of the service</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quote line items associated with the specified service</returns>
        Task<IEnumerable<QuoteLineItem>> GetByServiceIdAsync(Guid serviceId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the total amount for a specific quote
        /// </summary>
        /// <param name="quoteId">The unique identifier of the quote</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The total amount for the specified quote</returns>
        Task<decimal> GetTotalForQuoteAsync(Guid quoteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets quote line items that were created within a specific date range
        /// </summary>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quote line items created within the specified date range</returns>
        Task<IEnumerable<QuoteLineItem>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    }
}
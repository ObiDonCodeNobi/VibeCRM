using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Quote_QuoteLineItem junction entities
    /// </summary>
    public interface IQuoteQuoteLineItemRepository : IJunctionRepository<Quote_QuoteLineItem, Guid, Guid>
    {
        /// <summary>
        /// Gets all quote-quoteLineItem relationships for a specific quote
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quote-quoteLineItem relationships for the specified quote</returns>
        Task<IEnumerable<Quote_QuoteLineItem>> GetByQuoteIdAsync(Guid quoteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all quote-quoteLineItem relationships for a specific quote line item
        /// </summary>
        /// <param name="quoteLineItemId">The quote line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quote-quoteLineItem relationships for the specified quote line item</returns>
        Task<IEnumerable<Quote_QuoteLineItem>> GetByQuoteLineItemIdAsync(Guid quoteLineItemId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific quote-quoteLineItem relationship
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="quoteLineItemId">The quote line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The quote-quoteLineItem relationship if found, otherwise null</returns>
        Task<Quote_QuoteLineItem?> GetByQuoteAndQuoteLineItemIdAsync(Guid quoteId, Guid quoteLineItemId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a relationship exists between a quote and a quote line item
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="quoteLineItemId">The quote line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        Task<bool> ExistsByQuoteAndQuoteLineItemAsync(Guid quoteId, Guid quoteLineItemId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a specific quote-quoteLineItem relationship
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="quoteLineItemId">The quote line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        Task<bool> DeleteByQuoteAndQuoteLineItemIdAsync(Guid quoteId, Guid quoteLineItemId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all quote-quoteLineItem relationships for a specific quote
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByQuoteIdAsync(Guid quoteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all quote-quoteLineItem relationships for a specific quote line item
        /// </summary>
        /// <param name="quoteLineItemId">The quote line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByQuoteLineItemIdAsync(Guid quoteLineItemId, CancellationToken cancellationToken = default);
    }
}
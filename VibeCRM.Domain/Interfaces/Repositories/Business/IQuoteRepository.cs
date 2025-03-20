using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Business
{
    /// <summary>
    /// Repository interface for managing Quote entities
    /// </summary>
    public interface IQuoteRepository : IRepository<Quote, Guid>
    {
        /// <summary>
        /// Gets quotes by number
        /// </summary>
        /// <param name="number">The quote number</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quotes with the specified number</returns>
        Task<IEnumerable<Quote>> GetByNumberAsync(string number, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets quotes for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quotes associated with the specified company</returns>
        Task<IEnumerable<Quote>> GetByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets quotes associated with a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quotes associated with the specified activity</returns>
        Task<IEnumerable<Quote>> GetByActivityAsync(Guid activityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a quote by its unique identifier with all related entities loaded
        /// </summary>
        /// <param name="id">The unique identifier of the quote</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The quote with all related entities if found, otherwise null</returns>
        Task<Quote?> GetByIdWithRelatedEntitiesAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the quote status for a quote
        /// </summary>
        /// <param name="quote">The quote for which to load the quote status</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task LoadQuoteStatusAsync(Quote quote, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the line items for a quote
        /// </summary>
        /// <param name="quote">The quote for which to load the line items</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task LoadLineItemsAsync(Quote quote, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the sales orders for a quote
        /// </summary>
        /// <param name="quote">The quote for which to load the sales orders</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task LoadSalesOrdersAsync(Quote quote, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the companies associated with a quote
        /// </summary>
        /// <param name="quote">The quote for which to load the companies</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task LoadCompaniesAsync(Quote quote, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the activities associated with a quote
        /// </summary>
        /// <param name="quote">The quote for which to load the activities</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task LoadActivitiesAsync(Quote quote, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets quotes by quote status identifier
        /// </summary>
        /// <param name="quoteStatusId">The quote status identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quotes with the specified quote status</returns>
        Task<IEnumerable<Quote>> GetByQuoteStatusIdAsync(Guid quoteStatusId, CancellationToken cancellationToken = default);
    }
}
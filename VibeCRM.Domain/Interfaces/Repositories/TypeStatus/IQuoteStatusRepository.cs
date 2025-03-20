using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.TypeStatus
{
    /// <summary>
    /// Repository interface for managing QuoteStatus entities
    /// </summary>
    public interface IQuoteStatusRepository : IRepository<QuoteStatus, Guid>
    {
        /// <summary>
        /// Gets quote statuses ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quote statuses ordered by their ordinal position</returns>
        Task<IEnumerable<QuoteStatus>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets quote statuses by status name
        /// </summary>
        /// <param name="status">The status name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quote statuses with the specified status name</returns>
        Task<IEnumerable<QuoteStatus>> GetByStatusAsync(string status, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the default quote status
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default quote status, or null if no quote statuses exist</returns>
        Task<QuoteStatus?> GetDefaultAsync(CancellationToken cancellationToken = default);
    }
}
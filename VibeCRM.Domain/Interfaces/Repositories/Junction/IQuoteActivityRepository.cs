using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Quote_Activity junction entities
    /// </summary>
    public interface IQuoteActivityRepository : IJunctionRepository<Quote_Activity, Guid, Guid>
    {
        /// <summary>
        /// Gets all quote-activity relationships for a specific quote
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quote-activity relationships for the specified quote</returns>
        Task<IEnumerable<Quote_Activity>> GetByQuoteIdAsync(Guid quoteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all quote-activity relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quote-activity relationships for the specified activity</returns>
        Task<IEnumerable<Quote_Activity>> GetByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a relationship between the specified quote and activity exists
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if a relationship exists, otherwise false</returns>
        Task<bool> ExistsByQuoteAndActivityAsync(Guid quoteId, Guid activityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all quote-activity relationships for a specific quote
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByQuoteIdAsync(Guid quoteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all quote-activity relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a specific relationship between a quote and an activity
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was deleted, otherwise false</returns>
        Task<bool> DeleteByQuoteAndActivityAsync(Guid quoteId, Guid activityId, CancellationToken cancellationToken = default);
    }
}
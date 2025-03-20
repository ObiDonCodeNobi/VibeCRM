using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Person_Quote junction entities
    /// </summary>
    public interface IPersonQuoteRepository : IJunctionRepository<Person_Quote, Guid, Guid>
    {
        /// <summary>
        /// Gets all person-quote relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-quote relationships for the specified person</returns>
        Task<IEnumerable<Person_Quote>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all person-quote relationships for a specific quote
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-quote relationships for the specified quote</returns>
        Task<IEnumerable<Person_Quote>> GetByQuoteIdAsync(Guid quoteId, CancellationToken cancellationToken = default);
    }
}
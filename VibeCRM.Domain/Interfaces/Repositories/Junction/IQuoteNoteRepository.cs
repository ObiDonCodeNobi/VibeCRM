using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Quote_Note junction entities
    /// </summary>
    public interface IQuoteNoteRepository : IJunctionRepository<Quote_Note, Guid, Guid>
    {
        /// <summary>
        /// Gets all quote-note relationships for a specific quote
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quote-note relationships for the specified quote</returns>
        Task<IEnumerable<Quote_Note>> GetByQuoteIdAsync(Guid quoteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all quote-note relationships for a specific note
        /// </summary>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quote-note relationships for the specified note</returns>
        Task<IEnumerable<Quote_Note>> GetByNoteIdAsync(Guid noteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific quote-note relationship
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The quote-note relationship if found, otherwise null</returns>
        Task<Quote_Note?> GetByQuoteAndNoteIdAsync(Guid quoteId, Guid noteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a relationship exists between a quote and a note
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        Task<bool> ExistsByQuoteAndNoteAsync(Guid quoteId, Guid noteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a specific quote-note relationship
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        Task<bool> DeleteByQuoteAndNoteIdAsync(Guid quoteId, Guid noteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all quote-note relationships for a specific quote
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByQuoteIdAsync(Guid quoteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all quote-note relationships for a specific note
        /// </summary>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByNoteIdAsync(Guid noteId, CancellationToken cancellationToken = default);
    }
}
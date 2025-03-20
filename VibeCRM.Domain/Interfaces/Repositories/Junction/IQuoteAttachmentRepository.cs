using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Quote_Attachment junction entities
    /// </summary>
    public interface IQuoteAttachmentRepository : IJunctionRepository<Quote_Attachment, Guid, Guid>
    {
        /// <summary>
        /// Gets all quote-attachment relationships for a specific quote
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quote-attachment relationships for the specified quote</returns>
        Task<IEnumerable<Quote_Attachment>> GetByQuoteIdAsync(Guid quoteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all quote-attachment relationships for a specific attachment
        /// </summary>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of quote-attachment relationships for the specified attachment</returns>
        Task<IEnumerable<Quote_Attachment>> GetByAttachmentIdAsync(Guid attachmentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific quote-attachment relationship
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The quote-attachment relationship if found, otherwise null</returns>
        Task<Quote_Attachment?> GetByQuoteAndAttachmentIdAsync(Guid quoteId, Guid attachmentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a relationship exists between a quote and an attachment
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        Task<bool> ExistsByQuoteAndAttachmentAsync(Guid quoteId, Guid attachmentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a specific quote-attachment relationship
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        Task<bool> DeleteByQuoteAndAttachmentIdAsync(Guid quoteId, Guid attachmentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all quote-attachment relationships for a specific quote
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByQuoteIdAsync(Guid quoteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all quote-attachment relationships for a specific attachment
        /// </summary>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByAttachmentIdAsync(Guid attachmentId, CancellationToken cancellationToken = default);
    }
}
using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Invoice_Attachment junction entities
    /// </summary>
    public interface IInvoiceAttachmentRepository : IJunctionRepository<Invoice_Attachment, Guid, Guid>
    {
        /// <summary>
        /// Gets all invoice-attachment relationships for a specific invoice
        /// </summary>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoice-attachment relationships for the specified invoice</returns>
        Task<IEnumerable<Invoice_Attachment>> GetByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all invoice-attachment relationships for a specific attachment
        /// </summary>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoice-attachment relationships for the specified attachment</returns>
        Task<IEnumerable<Invoice_Attachment>> GetByAttachmentIdAsync(Guid attachmentId, CancellationToken cancellationToken = default);
    }
}
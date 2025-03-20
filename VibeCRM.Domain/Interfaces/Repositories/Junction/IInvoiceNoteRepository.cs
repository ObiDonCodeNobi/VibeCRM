using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Invoice_Note junction entities
    /// </summary>
    public interface IInvoiceNoteRepository : IJunctionRepository<Invoice_Note, Guid, Guid>
    {
        /// <summary>
        /// Gets all invoice-note relationships for a specific invoice
        /// </summary>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoice-note relationships for the specified invoice</returns>
        Task<IEnumerable<Invoice_Note>> GetByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all invoice-note relationships for a specific note
        /// </summary>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoice-note relationships for the specified note</returns>
        Task<IEnumerable<Invoice_Note>> GetByNoteIdAsync(Guid noteId, CancellationToken cancellationToken = default);
    }
}
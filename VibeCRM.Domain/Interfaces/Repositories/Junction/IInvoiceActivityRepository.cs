using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Invoice_Activity junction entities
    /// </summary>
    public interface IInvoiceActivityRepository : IJunctionRepository<Invoice_Activity, Guid, Guid>
    {
        /// <summary>
        /// Gets all invoice-activity relationships for a specific invoice
        /// </summary>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoice-activity relationships for the specified invoice</returns>
        Task<IEnumerable<Invoice_Activity>> GetByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all invoice-activity relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoice-activity relationships for the specified activity</returns>
        Task<IEnumerable<Invoice_Activity>> GetByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a relationship between the specified invoice and activity exists
        /// </summary>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists; otherwise, false</returns>
        Task<bool> ExistsAsync(Guid invoiceId, Guid activityId, CancellationToken cancellationToken = default);
    }
}
using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.TypeStatus
{
    /// <summary>
    /// Repository interface for managing InvoiceStatus entities
    /// </summary>
    public interface IInvoiceStatusRepository : IRepository<InvoiceStatus, Guid>
    {
        /// <summary>
        /// Gets invoice statuses ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoice statuses ordered by their ordinal position</returns>
        Task<IEnumerable<InvoiceStatus>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets invoice statuses by status name
        /// </summary>
        /// <param name="status">The status name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoice statuses with the specified status name</returns>
        Task<IEnumerable<InvoiceStatus>> GetByStatusAsync(string status, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the default invoice status
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default invoice status, or null if no invoice statuses exist</returns>
        Task<InvoiceStatus?> GetDefaultAsync(CancellationToken cancellationToken = default);
    }
}
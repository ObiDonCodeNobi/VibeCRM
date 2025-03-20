using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Person_Invoice junction entities
    /// </summary>
    public interface IPersonInvoiceRepository : IJunctionRepository<Person_Invoice, Guid, Guid>
    {
        /// <summary>
        /// Gets all person-invoice relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-invoice relationships for the specified person</returns>
        Task<IEnumerable<Person_Invoice>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all person-invoice relationships for a specific invoice
        /// </summary>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-invoice relationships for the specified invoice</returns>
        Task<IEnumerable<Person_Invoice>> GetByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default);
    }
}
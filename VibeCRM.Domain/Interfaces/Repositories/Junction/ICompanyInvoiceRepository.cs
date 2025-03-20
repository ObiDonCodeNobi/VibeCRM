using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Company_Invoice junction entities
    /// </summary>
    public interface ICompanyInvoiceRepository : IJunctionRepository<Company_Invoice, Guid, Guid>
    {
        /// <summary>
        /// Gets all company-invoice relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-invoice relationships for the specified company</returns>
        Task<IEnumerable<Company_Invoice>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all company-invoice relationships for a specific invoice
        /// </summary>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-invoice relationships for the specified invoice</returns>
        Task<IEnumerable<Company_Invoice>> GetByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific company-invoice relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The company-invoice relationship if found, otherwise null</returns>
        Task<Company_Invoice?> GetByCompanyAndInvoiceIdAsync(Guid companyId, Guid invoiceId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a relationship exists between a company and an invoice
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        Task<bool> ExistsByCompanyAndInvoiceAsync(Guid companyId, Guid invoiceId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a specific company-invoice relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        Task<bool> DeleteByCompanyAndInvoiceIdAsync(Guid companyId, Guid invoiceId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all company-invoice relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all company-invoice relationships for a specific invoice
        /// </summary>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default);
    }
}
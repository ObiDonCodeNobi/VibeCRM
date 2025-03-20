using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Invoice_InvoiceLineItem junction entities
    /// </summary>
    public interface IInvoiceInvoiceLineItemRepository : IJunctionRepository<Invoice_InvoiceLineItem, Guid, Guid>
    {
        /// <summary>
        /// Gets all invoice-invoiceLineItem relationships for a specific invoice
        /// </summary>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoice-invoiceLineItem relationships for the specified invoice</returns>
        Task<IEnumerable<Invoice_InvoiceLineItem>> GetByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all invoice-invoiceLineItem relationships for a specific invoice line item
        /// </summary>
        /// <param name="invoiceLineItemId">The invoice line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoice-invoiceLineItem relationships for the specified invoice line item</returns>
        Task<IEnumerable<Invoice_InvoiceLineItem>> GetByInvoiceLineItemIdAsync(Guid invoiceLineItemId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific invoice-invoiceLineItem relationship
        /// </summary>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="invoiceLineItemId">The invoice line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The invoice-invoiceLineItem relationship if found, otherwise null</returns>
        Task<Invoice_InvoiceLineItem?> GetByInvoiceAndInvoiceLineItemIdAsync(Guid invoiceId, Guid invoiceLineItemId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a relationship exists between an invoice and an invoice line item
        /// </summary>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="invoiceLineItemId">The invoice line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        Task<bool> ExistsByInvoiceAndInvoiceLineItemAsync(Guid invoiceId, Guid invoiceLineItemId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a specific invoice-invoiceLineItem relationship
        /// </summary>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="invoiceLineItemId">The invoice line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        Task<bool> DeleteByInvoiceAndInvoiceLineItemIdAsync(Guid invoiceId, Guid invoiceLineItemId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all invoice-invoiceLineItem relationships for a specific invoice
        /// </summary>
        /// <param name="invoiceId">The invoice identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all invoice-invoiceLineItem relationships for a specific invoice line item
        /// </summary>
        /// <param name="invoiceLineItemId">The invoice line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByInvoiceLineItemIdAsync(Guid invoiceLineItemId, CancellationToken cancellationToken = default);
    }
}
using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Business
{
    /// <summary>
    /// Repository interface for managing Invoice entities
    /// </summary>
    public interface IInvoiceRepository : IRepository<Invoice, Guid>
    {
        /// <summary>
        /// Gets invoices by their sales order identifier
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoices for the specified sales order</returns>
        Task<IEnumerable<Invoice>> GetBySalesOrderIdAsync(Guid salesOrderId, CancellationToken cancellationToken = default);
    }
}
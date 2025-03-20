using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing SalesOrderLineItem_Service junction entities
    /// </summary>
    public interface ISalesOrderLineItemServiceRepository : IJunctionRepository<SalesOrderLineItem_Service, Guid, Guid>
    {
        /// <summary>
        /// Gets all salesOrderLineItem-service relationships for a specific sales order line item
        /// </summary>
        /// <param name="salesOrderLineItemId">The sales order line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of salesOrderLineItem-service relationships for the specified sales order line item</returns>
        Task<IEnumerable<SalesOrderLineItem_Service>> GetBySalesOrderLineItemIdAsync(Guid salesOrderLineItemId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all salesOrderLineItem-service relationships for a specific service
        /// </summary>
        /// <param name="serviceId">The service identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of salesOrderLineItem-service relationships for the specified service</returns>
        Task<IEnumerable<SalesOrderLineItem_Service>> GetByServiceIdAsync(Guid serviceId, CancellationToken cancellationToken = default);
    }
}
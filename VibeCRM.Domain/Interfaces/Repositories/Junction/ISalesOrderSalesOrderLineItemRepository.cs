using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing SalesOrder_SalesOrderLineItem junction entities
    /// </summary>
    public interface ISalesOrderSalesOrderLineItemRepository : IJunctionRepository<SalesOrder_SalesOrderLineItem, Guid, Guid>
    {
        /// <summary>
        /// Gets all salesOrder-salesOrderLineItem relationships for a specific sales order
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of salesOrder-salesOrderLineItem relationships for the specified sales order</returns>
        Task<IEnumerable<SalesOrder_SalesOrderLineItem>> GetBySalesOrderIdAsync(Guid salesOrderId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all salesOrder-salesOrderLineItem relationships for a specific sales order line item
        /// </summary>
        /// <param name="salesOrderLineItemId">The sales order line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of salesOrder-salesOrderLineItem relationships for the specified sales order line item</returns>
        Task<IEnumerable<SalesOrder_SalesOrderLineItem>> GetBySalesOrderLineItemIdAsync(Guid salesOrderLineItemId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific salesOrder-salesOrderLineItem relationship
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="salesOrderLineItemId">The sales order line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The salesOrder-salesOrderLineItem relationship if found, otherwise null</returns>
        Task<SalesOrder_SalesOrderLineItem?> GetBySalesOrderAndSalesOrderLineItemIdAsync(Guid salesOrderId, Guid salesOrderLineItemId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a relationship exists between a sales order and a sales order line item
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="salesOrderLineItemId">The sales order line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        Task<bool> ExistsBySalesOrderAndSalesOrderLineItemAsync(Guid salesOrderId, Guid salesOrderLineItemId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a specific salesOrder-salesOrderLineItem relationship
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="salesOrderLineItemId">The sales order line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        Task<bool> DeleteBySalesOrderAndSalesOrderLineItemIdAsync(Guid salesOrderId, Guid salesOrderLineItemId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all salesOrder-salesOrderLineItem relationships for a specific sales order
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteBySalesOrderIdAsync(Guid salesOrderId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all salesOrder-salesOrderLineItem relationships for a specific sales order line item
        /// </summary>
        /// <param name="salesOrderLineItemId">The sales order line item identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteBySalesOrderLineItemIdAsync(Guid salesOrderLineItemId, CancellationToken cancellationToken = default);
    }
}
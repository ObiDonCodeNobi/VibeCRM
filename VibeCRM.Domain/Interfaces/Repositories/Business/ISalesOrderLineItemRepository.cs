using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Business
{
    /// <summary>
    /// Repository interface for managing SalesOrderLineItem entities
    /// </summary>
    public interface ISalesOrderLineItemRepository : IRepository<SalesOrderLineItem, Guid>
    {
        /// <summary>
        /// Gets sales order line items for a specific sales order
        /// </summary>
        /// <param name="salesOrderId">The unique identifier of the sales order</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales order line items associated with the specified sales order</returns>
        Task<IEnumerable<SalesOrderLineItem>> GetBySalesOrderIdAsync(Guid salesOrderId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets sales order line items for a specific product
        /// </summary>
        /// <param name="productId">The unique identifier of the product</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales order line items associated with the specified product</returns>
        Task<IEnumerable<SalesOrderLineItem>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets sales order line items for a specific service
        /// </summary>
        /// <param name="serviceId">The unique identifier of the service</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales order line items associated with the specified service</returns>
        Task<IEnumerable<SalesOrderLineItem>> GetByServiceIdAsync(Guid serviceId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the total amount for a specific sales order
        /// </summary>
        /// <param name="salesOrderId">The unique identifier of the sales order</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The total amount for the specified sales order</returns>
        Task<decimal> GetTotalForSalesOrderAsync(Guid salesOrderId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets sales order line items that were created within a specific date range
        /// </summary>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales order line items created within the specified date range</returns>
        Task<IEnumerable<SalesOrderLineItem>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    }
}
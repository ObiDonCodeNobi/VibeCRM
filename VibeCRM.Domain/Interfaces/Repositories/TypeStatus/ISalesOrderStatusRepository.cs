using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.TypeStatus
{
    /// <summary>
    /// Repository interface for accessing and managing sales order status entities
    /// </summary>
    public interface ISalesOrderStatusRepository : IRepository<SalesOrderStatus, Guid>
    {
        /// <summary>
        /// Gets sales order statuses ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales order statuses ordered by their ordinal position</returns>
        Task<IEnumerable<SalesOrderStatus>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets sales order statuses by status name
        /// </summary>
        /// <param name="status">The status name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales order statuses with the specified status name</returns>
        Task<IEnumerable<SalesOrderStatus>> GetByStatusAsync(string status, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the default sales order status
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default sales order status, or null if no sales order statuses exist</returns>
        Task<SalesOrderStatus?> GetDefaultAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets open sales order statuses that represent active orders
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales order statuses that represent open orders</returns>
        Task<IEnumerable<SalesOrderStatus>> GetOpenStatusesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets completed sales order statuses
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales order statuses that represent completed orders</returns>
        Task<IEnumerable<SalesOrderStatus>> GetCompletedStatusesAsync(CancellationToken cancellationToken = default);
    }
}
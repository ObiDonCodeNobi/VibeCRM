using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing SalesOrder_Activity junction entities
    /// </summary>
    public interface ISalesOrderActivityRepository : IJunctionRepository<SalesOrder_Activity, Guid, Guid>
    {
        /// <summary>
        /// Gets all sales order-activity relationships for a specific sales order
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales order-activity relationships for the specified sales order</returns>
        Task<IEnumerable<SalesOrder_Activity>> GetBySalesOrderIdAsync(Guid salesOrderId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all sales order-activity relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales order-activity relationships for the specified activity</returns>
        Task<IEnumerable<SalesOrder_Activity>> GetByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all sales order-activity relationships for a specific sales order
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteBySalesOrderIdAsync(Guid salesOrderId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all sales order-activity relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a specific relationship between a sales order and an activity
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was deleted, otherwise false</returns>
        Task<bool> DeleteBySalesOrderAndActivityAsync(Guid salesOrderId, Guid activityId, CancellationToken cancellationToken = default);
    }
}
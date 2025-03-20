using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing SalesOrder_Attachment junction entities
    /// </summary>
    public interface ISalesOrderAttachmentRepository : IJunctionRepository<SalesOrder_Attachment, Guid, Guid>
    {
        /// <summary>
        /// Gets all salesOrder-attachment relationships for a specific sales order
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of salesOrder-attachment relationships for the specified sales order</returns>
        Task<IEnumerable<SalesOrder_Attachment>> GetBySalesOrderIdAsync(Guid salesOrderId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all salesOrder-attachment relationships for a specific attachment
        /// </summary>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of salesOrder-attachment relationships for the specified attachment</returns>
        Task<IEnumerable<SalesOrder_Attachment>> GetByAttachmentIdAsync(Guid attachmentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific salesOrder-attachment relationship
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The salesOrder-attachment relationship if found, otherwise null</returns>
        Task<SalesOrder_Attachment?> GetBySalesOrderAndAttachmentIdAsync(Guid salesOrderId, Guid attachmentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a relationship exists between a sales order and an attachment
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        Task<bool> ExistsBySalesOrderAndAttachmentAsync(Guid salesOrderId, Guid attachmentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a specific salesOrder-attachment relationship
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        Task<bool> DeleteBySalesOrderAndAttachmentIdAsync(Guid salesOrderId, Guid attachmentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all salesOrder-attachment relationships for a specific sales order
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteBySalesOrderIdAsync(Guid salesOrderId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all salesOrder-attachment relationships for a specific attachment
        /// </summary>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByAttachmentIdAsync(Guid attachmentId, CancellationToken cancellationToken = default);
    }
}
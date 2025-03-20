using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing SalesOrder_Note junction entities
    /// </summary>
    public interface ISalesOrderNoteRepository : IJunctionRepository<SalesOrder_Note, Guid, Guid>
    {
        /// <summary>
        /// Gets all salesOrder-note relationships for a specific sales order
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of salesOrder-note relationships for the specified sales order</returns>
        Task<IEnumerable<SalesOrder_Note>> GetBySalesOrderIdAsync(Guid salesOrderId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all salesOrder-note relationships for a specific note
        /// </summary>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of salesOrder-note relationships for the specified note</returns>
        Task<IEnumerable<SalesOrder_Note>> GetByNoteIdAsync(Guid noteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific salesOrder-note relationship
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The salesOrder-note relationship if found, otherwise null</returns>
        Task<SalesOrder_Note?> GetBySalesOrderAndNoteIdAsync(Guid salesOrderId, Guid noteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a relationship exists between a sales order and a note
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        Task<bool> ExistsBySalesOrderAndNoteAsync(Guid salesOrderId, Guid noteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a specific salesOrder-note relationship
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        Task<bool> DeleteBySalesOrderAndNoteIdAsync(Guid salesOrderId, Guid noteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all salesOrder-note relationships for a specific sales order
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteBySalesOrderIdAsync(Guid salesOrderId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all salesOrder-note relationships for a specific note
        /// </summary>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByNoteIdAsync(Guid noteId, CancellationToken cancellationToken = default);
    }
}
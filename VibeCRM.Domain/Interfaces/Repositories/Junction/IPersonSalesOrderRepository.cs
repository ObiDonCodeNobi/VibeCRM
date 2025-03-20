using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Person_SalesOrder junction entities
    /// </summary>
    public interface IPersonSalesOrderRepository : IJunctionRepository<Person_SalesOrder, Guid, Guid>
    {
        /// <summary>
        /// Gets all person-salesOrder relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-salesOrder relationships for the specified person</returns>
        Task<IEnumerable<Person_SalesOrder>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all person-salesOrder relationships for a specific sales order
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-salesOrder relationships for the specified sales order</returns>
        Task<IEnumerable<Person_SalesOrder>> GetBySalesOrderIdAsync(Guid salesOrderId, CancellationToken cancellationToken = default);
    }
}
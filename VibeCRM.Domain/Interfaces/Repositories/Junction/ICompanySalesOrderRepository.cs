using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Company_SalesOrder junction entities
    /// </summary>
    public interface ICompanySalesOrderRepository : IJunctionRepository<Company_SalesOrder, Guid, Guid>
    {
        /// <summary>
        /// Gets all company-sales order relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-sales order relationships for the specified company</returns>
        Task<IEnumerable<Company_SalesOrder>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all company-sales order relationships for a specific sales order
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-sales order relationships for the specified sales order</returns>
        Task<IEnumerable<Company_SalesOrder>> GetBySalesOrderIdAsync(Guid salesOrderId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific company-sales order relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The company-sales order relationship if found, otherwise null</returns>
        Task<Company_SalesOrder?> GetByCompanyAndSalesOrderIdAsync(Guid companyId, Guid salesOrderId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all company-sales order relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all company-sales order relationships for a specific sales order
        /// </summary>
        /// <param name="salesOrderId">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteBySalesOrderIdAsync(Guid salesOrderId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets company-sales order relationships by date range
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-sales order relationships within the specified date range</returns>
        Task<IEnumerable<Company_SalesOrder>> GetByDateRangeAsync(Guid companyId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets company-sales order relationships by sales order status
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="salesOrderStatusId">The sales order status identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-sales order relationships with the specified status</returns>
        Task<IEnumerable<Company_SalesOrder>> GetBySalesOrderStatusAsync(Guid companyId, Guid salesOrderStatusId, CancellationToken cancellationToken = default);
    }
}
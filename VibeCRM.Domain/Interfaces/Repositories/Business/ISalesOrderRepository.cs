using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Business
{
    /// <summary>
    /// Repository interface for managing SalesOrder entities
    /// </summary>
    public interface ISalesOrderRepository : IRepository<SalesOrder, Guid>
    {
        /// <summary>
        /// Gets sales orders by number
        /// </summary>
        /// <param name="number">The sales order number</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales orders with the specified number</returns>
        Task<IEnumerable<SalesOrder>> GetByNumberAsync(string number, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets sales orders by status
        /// </summary>
        /// <param name="salesOrderStatusId">The sales order status identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales orders with the specified status</returns>
        Task<IEnumerable<SalesOrder>> GetBySalesOrderStatusAsync(Guid salesOrderStatusId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets sales orders by order date range
        /// </summary>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales orders with order dates within the specified range</returns>
        /// <exception cref="ArgumentException">Thrown when startDate is later than endDate</exception>
        Task<IEnumerable<SalesOrder>> GetByOrderDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a sales order by its unique identifier with related entities
        /// </summary>
        /// <param name="id">The sales order identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The sales order with the specified identifier including all related entities, or null if not found</returns>
        /// <exception cref="ArgumentException">Thrown when id is empty</exception>
        Task<SalesOrder> GetByIdWithRelatedEntitiesAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets sales orders for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales orders associated with the specified company</returns>
        Task<IEnumerable<SalesOrder>> GetByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets sales orders created from a specific quote
        /// </summary>
        /// <param name="quoteId">The quote identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales orders created from the specified quote</returns>
        Task<IEnumerable<SalesOrder>> GetByQuoteAsync(Guid quoteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets sales orders associated with a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales orders associated with the specified activity</returns>
        Task<IEnumerable<SalesOrder>> GetByActivityAsync(Guid activityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the sales order status for a sales order
        /// </summary>
        /// <param name="salesOrder">The sales order to load the status for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when salesOrder is null</exception>
        Task LoadSalesOrderStatusAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the ship method for a sales order
        /// </summary>
        /// <param name="salesOrder">The sales order to load the ship method for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when salesOrder is null</exception>
        Task LoadShipMethodAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the tax code for a sales order
        /// </summary>
        /// <param name="salesOrder">The sales order to load the tax code for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when salesOrder is null</exception>
        Task LoadTaxCodeAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the bill-to and ship-to addresses for a sales order
        /// </summary>
        /// <param name="salesOrder">The sales order to load the addresses for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when salesOrder is null</exception>
        Task LoadAddressesAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the line items for a sales order
        /// </summary>
        /// <param name="salesOrder">The sales order to load the line items for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when salesOrder is null</exception>
        Task LoadLineItemsAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the companies associated with a sales order
        /// </summary>
        /// <param name="salesOrder">The sales order to load the companies for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when salesOrder is null</exception>
        Task LoadCompaniesAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the activities associated with a sales order
        /// </summary>
        /// <param name="salesOrder">The sales order to load the activities for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when salesOrder is null</exception>
        Task LoadActivitiesAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the quote associated with a sales order
        /// </summary>
        /// <param name="salesOrder">The sales order to load the quote for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">Thrown when salesOrder is null</exception>
        Task LoadQuoteAsync(SalesOrder salesOrder, CancellationToken cancellationToken = default);
    }
}
using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Business
{
    /// <summary>
    /// Repository interface for managing Service entities
    /// </summary>
    public interface IServiceRepository : IRepository<Service, Guid>
    {
        /// <summary>
        /// Gets services by their type
        /// </summary>
        /// <param name="serviceTypeId">The service type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of services with the specified type</returns>
        Task<IEnumerable<Service>> GetByServiceTypeAsync(Guid serviceTypeId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets services by name or partial name match
        /// </summary>
        /// <param name="name">The name or partial name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of services matching the specified name pattern</returns>
        Task<IEnumerable<Service>> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a service exists with the given name
        /// </summary>
        /// <param name="name">The service name to check</param>
        /// <param name="excludeId">Optional service ID to exclude from the check (for updates)</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if a service with the name exists, false otherwise</returns>
        Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets active services ordered by name
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of active services ordered by name</returns>
        Task<IEnumerable<Service>> GetActiveOrderedAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all active services with related entities loaded
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of active services with all related entities loaded</returns>
        Task<IEnumerable<Service>> GetAllWithRelatedEntitiesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads quote line items associated with a service
        /// </summary>
        /// <param name="service">The service to load quote line items for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task LoadQuoteLineItemsAsync(Service service, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads invoice line items associated with a service
        /// </summary>
        /// <param name="service">The service to load invoice line items for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task LoadInvoiceLineItemsAsync(Service service, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads sales order line item service relationships associated with a service
        /// </summary>
        /// <param name="service">The service to load sales order line item service relationships for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task LoadSalesOrderLineItemServicesAsync(Service service, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads sales order line items associated with a service
        /// </summary>
        /// <param name="service">The service to load sales order line items for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task LoadSalesOrderLineItemsAsync(Service service, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a service by ID with all related entities loaded
        /// </summary>
        /// <param name="id">The unique identifier of the service to retrieve</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The service with all related entities loaded if found, otherwise null</returns>
        Task<Service?> GetByIdWithRelatedEntitiesAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
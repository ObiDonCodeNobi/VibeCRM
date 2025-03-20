using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Business
{
    /// <summary>
    /// Repository interface for managing Product entities
    /// </summary>
    public interface IProductRepository : IRepository<Product, Guid>
    {
        /// <summary>
        /// Gets a product by its name
        /// </summary>
        /// <param name="name">The name of the product</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The product if found, otherwise null</returns>
        Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all products of a specific product type
        /// </summary>
        /// <param name="productTypeId">The unique identifier of the product type</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of products of the specified type</returns>
        Task<IEnumerable<Product>> GetByProductTypeIdAsync(Guid productTypeId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all products that belong to a specific product group
        /// </summary>
        /// <param name="productGroupId">The unique identifier of the product group</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of products that belong to the specified product group</returns>
        Task<IEnumerable<Product>> GetByProductGroupIdAsync(Guid productGroupId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a product by its unique identifier with all related entities loaded
        /// </summary>
        /// <param name="id">The unique identifier of the product</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The product with all related entities if found, otherwise null</returns>
        Task<Product?> GetByIdWithRelatedEntitiesAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the product type for a product
        /// </summary>
        /// <param name="product">The product to load the product type for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task LoadProductTypeAsync(Product product, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the quote line items for a product
        /// </summary>
        /// <param name="product">The product to load the quote line items for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task LoadQuoteLineItemsAsync(Product product, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the sales order line items for a product
        /// </summary>
        /// <param name="product">The product to load the sales order line items for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task LoadSalesOrderLineItemsAsync(Product product, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the product groups for a product
        /// </summary>
        /// <param name="product">The product to load the product groups for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task LoadProductGroupsAsync(Product product, CancellationToken cancellationToken = default);
    }
}
using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Business
{
    /// <summary>
    /// Repository interface for managing ProductGroup entities
    /// </summary>
    public interface IProductGroupRepository : IRepository<ProductGroup, Guid>
    {
        /// <summary>
        /// Gets a product group by its name
        /// </summary>
        /// <param name="name">The name of the product group</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The product group if found, otherwise null</returns>
        Task<ProductGroup?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all product groups that contain a specific product
        /// </summary>
        /// <param name="productId">The unique identifier of the product</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of product groups containing the specified product</returns>
        Task<IEnumerable<ProductGroup>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all product groups within a specific parent group
        /// </summary>
        /// <param name="parentGroupId">The unique identifier of the parent product group</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of product groups that are children of the specified parent group</returns>
        Task<IEnumerable<ProductGroup>> GetByParentGroupIdAsync(Guid parentGroupId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all root-level product groups (those with no parent)
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of root-level product groups</returns>
        Task<IEnumerable<ProductGroup>> GetRootGroupsAsync(CancellationToken cancellationToken = default);
    }
}
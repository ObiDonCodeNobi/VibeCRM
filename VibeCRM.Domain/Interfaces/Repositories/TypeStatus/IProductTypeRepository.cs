using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.TypeStatus
{
    /// <summary>
    /// Repository interface for accessing and managing product type entities
    /// </summary>
    public interface IProductTypeRepository : IRepository<ProductType, Guid>
    {
        /// <summary>
        /// Gets product types ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of product types ordered by their ordinal position</returns>
        Task<IEnumerable<ProductType>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets product types by type name
        /// </summary>
        /// <param name="type">The type name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of product types with the specified type name</returns>
        Task<IEnumerable<ProductType>> GetByTypeAsync(string type, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the default product type
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default product type, or null if no product types exist</returns>
        Task<ProductType?> GetDefaultAsync(CancellationToken cancellationToken = default);
    }
}
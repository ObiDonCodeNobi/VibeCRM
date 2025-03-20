using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.TypeStatus
{
    /// <summary>
    /// Repository interface for managing ServiceType entities
    /// </summary>
    public interface IServiceTypeRepository : IRepository<ServiceType, Guid>
    {
        /// <summary>
        /// Gets service types ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of service types ordered by their ordinal position</returns>
        Task<IEnumerable<ServiceType>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets service types by type
        /// </summary>
        /// <param name="type">The type name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of service types with the specified type name</returns>
        Task<IEnumerable<ServiceType>> GetByTypeAsync(string type, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the default service type (typically the one with the lowest ordinal position)
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default service type or null if not found</returns>
        Task<ServiceType?> GetDefaultAsync(CancellationToken cancellationToken = default);
    }
}
using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.TypeStatus
{
    /// <summary>
    /// Repository interface for managing ActivityType entities
    /// </summary>
    public interface IActivityTypeRepository : IRepository<ActivityType, Guid>
    {
        /// <summary>
        /// Gets activity types ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activity types ordered by their ordinal position</returns>
        Task<IEnumerable<ActivityType>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets activity types by type name
        /// </summary>
        /// <param name="type">The type name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activity types with the specified type name</returns>
        Task<IEnumerable<ActivityType>> GetByTypeAsync(string type, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the default activity type (typically the one with the lowest ordinal position)
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default activity type or null if not found</returns>
        Task<ActivityType?> GetDefaultAsync(CancellationToken cancellationToken = default);
    }
}
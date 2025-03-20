using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.TypeStatus
{
    /// <summary>
    /// Repository interface for managing ActivityStatus entities
    /// </summary>
    public interface IActivityStatusRepository : IRepository<ActivityStatus, Guid>
    {
        /// <summary>
        /// Gets activity statuses ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activity statuses ordered by their ordinal position</returns>
        Task<IEnumerable<ActivityStatus>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets activity statuses by status name
        /// </summary>
        /// <param name="status">The status name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activity statuses with the specified status name</returns>
        Task<IEnumerable<ActivityStatus>> GetByStatusAsync(string status, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the default activity status
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default activity status, or null if no activity statuses exist</returns>
        Task<ActivityStatus?> GetDefaultAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets completed activity statuses
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activity statuses that represent completed activities</returns>
        Task<IEnumerable<ActivityStatus>> GetCompletedStatusesAsync(CancellationToken cancellationToken = default);
    }
}
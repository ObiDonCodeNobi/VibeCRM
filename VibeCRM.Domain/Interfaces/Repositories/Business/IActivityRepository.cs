using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Business
{
    /// <summary>
    /// Repository interface for managing Activity entities.
    /// Implements the soft delete pattern using the Active property.
    /// </summary>
    public interface IActivityRepository : IRepository<Activity, Guid>
    {
        /// <summary>
        /// Gets activities by their type.
        /// Only returns activities where Active = true (not soft-deleted).
        /// </summary>
        /// <param name="activityTypeId">The activity type identifier.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of activities with the specified type.</returns>
        Task<IEnumerable<Activity>> GetByActivityTypeAsync(Guid activityTypeId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets activities by their status.
        /// Only returns activities where Active = true (not soft-deleted).
        /// </summary>
        /// <param name="activityStatusId">The activity status identifier.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of activities with the specified status.</returns>
        Task<IEnumerable<Activity>> GetByActivityStatusAsync(Guid activityStatusId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets activities assigned to a specific user.
        /// Only returns activities where Active = true (not soft-deleted).
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of activities assigned to the specified user.</returns>
        Task<IEnumerable<Activity>> GetByAssignedUserAsync(Guid userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets activities assigned to a specific team.
        /// Only returns activities where Active = true (not soft-deleted).
        /// </summary>
        /// <param name="teamId">The team identifier.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of activities assigned to the specified team.</returns>
        Task<IEnumerable<Activity>> GetByAssignedTeamAsync(Guid teamId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets activities due within a specified date range.
        /// Only returns activities where Active = true (not soft-deleted).
        /// </summary>
        /// <param name="startDate">The start date of the range.</param>
        /// <param name="endDate">The end date of the range.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of activities due within the specified date range.</returns>
        Task<IEnumerable<Activity>> GetByDueDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets completed activities.
        /// Only returns activities where Active = true (not soft-deleted).
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of completed activities.</returns>
        Task<IEnumerable<Activity>> GetCompletedActivitiesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets incomplete activities.
        /// Only returns activities where Active = true (not soft-deleted).
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of incomplete activities.</returns>
        Task<IEnumerable<Activity>> GetIncompleteActivitiesAsync(CancellationToken cancellationToken = default);
    }
}
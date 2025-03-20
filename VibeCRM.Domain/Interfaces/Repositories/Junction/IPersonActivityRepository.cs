using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Person_Activity junction entities
    /// </summary>
    public interface IPersonActivityRepository : IJunctionRepository<Person_Activity, Guid, Guid>
    {
        /// <summary>
        /// Gets all person-activity relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-activity relationships for the specified person</returns>
        Task<IEnumerable<Person_Activity>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all person-activity relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-activity relationships for the specified activity</returns>
        Task<IEnumerable<Person_Activity>> GetByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific person-activity relationship
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The person-activity relationship if found, otherwise null</returns>
        Task<Person_Activity?> GetByPersonAndActivityIdAsync(Guid personId, Guid activityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a relationship exists between a person and an activity
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        Task<bool> ExistsByPersonAndActivityAsync(Guid personId, Guid activityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a specific person-activity relationship
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        Task<bool> DeleteByPersonAndActivityIdAsync(Guid personId, Guid activityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all person-activity relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all person-activity relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default);
    }
}
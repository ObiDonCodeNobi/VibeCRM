using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Activity_Note junction entities
    /// </summary>
    public interface IActivityNoteRepository : IJunctionRepository<Activity_Note, Guid, Guid>
    {
        /// <summary>
        /// Gets all activity-note relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activity-note relationships for the specified activity</returns>
        Task<IEnumerable<Activity_Note>> GetByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all activity-note relationships for a specific note
        /// </summary>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activity-note relationships for the specified note</returns>
        Task<IEnumerable<Activity_Note>> GetByNoteIdAsync(Guid noteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific activity-note relationship
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The activity-note relationship if found, otherwise null</returns>
        Task<Activity_Note?> GetByActivityAndNoteIdAsync(Guid activityId, Guid noteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a relationship exists between an activity and a note
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        Task<bool> ExistsByActivityAndNoteAsync(Guid activityId, Guid noteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a specific activity-note relationship
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        Task<bool> DeleteByActivityAndNoteIdAsync(Guid activityId, Guid noteId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all activity-note relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all activity-note relationships for a specific note
        /// </summary>
        /// <param name="noteId">The note identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByNoteIdAsync(Guid noteId, CancellationToken cancellationToken = default);
    }
}
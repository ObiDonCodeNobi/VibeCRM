using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Activity_Attachment junction entities
    /// </summary>
    public interface IActivityAttachmentRepository : IJunctionRepository<Activity_Attachment, Guid, Guid>
    {
        /// <summary>
        /// Gets all activity-attachment relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activity-attachment relationships for the specified activity</returns>
        Task<IEnumerable<Activity_Attachment>> GetByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all activity-attachment relationships for a specific attachment
        /// </summary>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activity-attachment relationships for the specified attachment</returns>
        Task<IEnumerable<Activity_Attachment>> GetByAttachmentIdAsync(Guid attachmentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific activity-attachment relationship
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The activity-attachment relationship if found, otherwise null</returns>
        Task<Activity_Attachment?> GetByActivityAndAttachmentIdAsync(Guid activityId, Guid attachmentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a relationship exists between an activity and an attachment
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        Task<bool> ExistsByActivityAndAttachmentAsync(Guid activityId, Guid attachmentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a specific activity-attachment relationship
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        Task<bool> DeleteByActivityAndAttachmentIdAsync(Guid activityId, Guid attachmentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all activity-attachment relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all activity-attachment relationships for a specific attachment
        /// </summary>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByAttachmentIdAsync(Guid attachmentId, CancellationToken cancellationToken = default);
    }
}
using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Workflow_Activity junction entities
    /// </summary>
    public interface IWorkflowActivityRepository : IJunctionRepository<Workflow_Activity, Guid, Guid>
    {
        /// <summary>
        /// Gets all workflow-activity relationships for a specific workflow
        /// </summary>
        /// <param name="workflowId">The workflow identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of workflow-activity relationships for the specified workflow</returns>
        Task<IEnumerable<Workflow_Activity>> GetByWorkflowIdAsync(Guid workflowId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all workflow-activity relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of workflow-activity relationships for the specified activity</returns>
        Task<IEnumerable<Workflow_Activity>> GetByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific workflow-activity relationship
        /// </summary>
        /// <param name="workflowId">The workflow identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The workflow-activity relationship if found, otherwise null</returns>
        Task<Workflow_Activity?> GetByWorkflowAndActivityIdAsync(Guid workflowId, Guid activityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a relationship exists between a workflow and an activity
        /// </summary>
        /// <param name="workflowId">The workflow identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        Task<bool> ExistsByWorkflowAndActivityAsync(Guid workflowId, Guid activityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a specific workflow-activity relationship
        /// </summary>
        /// <param name="workflowId">The workflow identifier</param>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        Task<bool> DeleteByWorkflowAndActivityIdAsync(Guid workflowId, Guid activityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all workflow-activity relationships for a specific workflow
        /// </summary>
        /// <param name="workflowId">The workflow identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByWorkflowIdAsync(Guid workflowId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all workflow-activity relationships for a specific activity
        /// </summary>
        /// <param name="activityId">The activity identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default);
    }
}
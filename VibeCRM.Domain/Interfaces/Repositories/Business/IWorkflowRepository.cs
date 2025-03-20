using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Business
{
    /// <summary>
    /// Repository interface for managing Workflow entities
    /// </summary>
    public interface IWorkflowRepository : IRepository<Workflow, Guid>
    {
        /// <summary>
        /// Gets a workflow by its name
        /// </summary>
        /// <param name="name">The name of the workflow</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The workflow if found, otherwise null</returns>
        Task<Workflow?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all workflows associated with a specific activity
        /// </summary>
        /// <param name="activityId">The unique identifier of the activity</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of workflows associated with the specified activity</returns>
        Task<IEnumerable<Workflow>> GetByActivityIdAsync(Guid activityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets workflows by their workflow type
        /// </summary>
        /// <param name="workflowTypeId">The workflow type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of workflows of the specified type</returns>
        Task<IEnumerable<Workflow>> GetByWorkflowTypeAsync(Guid workflowTypeId, CancellationToken cancellationToken = default);
    }
}
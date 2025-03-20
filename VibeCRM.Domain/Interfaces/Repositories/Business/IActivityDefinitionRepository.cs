using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Business
{
    /// <summary>
    /// Repository interface for managing ActivityDefinition entities
    /// </summary>
    public interface IActivityDefinitionRepository : IRepository<ActivityDefinition, Guid>
    {
        /// <summary>
        /// Gets an activity definition by its name
        /// </summary>
        /// <param name="name">The name of the activity definition</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The activity definition if found, otherwise null</returns>
        Task<ActivityDefinition?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets activity definitions by their activity type
        /// </summary>
        /// <param name="activityTypeId">The activity type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activity definitions of the specified type</returns>
        Task<IEnumerable<ActivityDefinition>> GetByActivityTypeAsync(Guid activityTypeId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets activity definitions associated with a specific workflow
        /// </summary>
        /// <param name="workflowId">The unique identifier of the workflow</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activity definitions associated with the specified workflow</returns>
        Task<IEnumerable<ActivityDefinition>> GetByWorkflowIdAsync(Guid workflowId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets activity definitions created by a specific user
        /// </summary>
        /// <param name="userId">The unique identifier of the user</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of activity definitions created by the specified user</returns>
        Task<IEnumerable<ActivityDefinition>> GetByCreatedByAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
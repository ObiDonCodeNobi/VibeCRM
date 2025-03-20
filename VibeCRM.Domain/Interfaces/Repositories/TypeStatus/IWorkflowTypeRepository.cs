using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.TypeStatus
{
    /// <summary>
    /// Repository interface for managing WorkflowType entities
    /// </summary>
    public interface IWorkflowTypeRepository : IRepository<WorkflowType, Guid>
    {
        /// <summary>
        /// Gets workflow types ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of workflow types ordered by their ordinal position</returns>
        Task<IEnumerable<WorkflowType>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets workflow types by type name
        /// </summary>
        /// <param name="type">The type name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of workflow types with the specified type name</returns>
        Task<IEnumerable<WorkflowType>> GetByTypeAsync(string type, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the default workflow type
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default workflow type, or null if no workflow types exist</returns>
        Task<WorkflowType?> GetDefaultAsync(CancellationToken cancellationToken = default);
    }
}
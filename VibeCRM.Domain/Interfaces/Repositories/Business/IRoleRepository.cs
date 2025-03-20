using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Business
{
    /// <summary>
    /// Repository interface for managing Role entities
    /// </summary>
    public interface IRoleRepository : IRepository<Role, Guid>
    {
        /// <summary>
        /// Gets a role by its name
        /// </summary>
        /// <param name="name">The name of the role</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The role if found, otherwise null</returns>
        Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all roles assigned to a specific user
        /// </summary>
        /// <param name="userId">The unique identifier of the user</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of roles assigned to the specified user</returns>
        Task<IEnumerable<Role>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
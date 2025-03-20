using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing User_Role junction entities
    /// </summary>
    public interface IUserRoleRepository : IJunctionRepository<User_Role, Guid, Guid>
    {
        /// <summary>
        /// Gets all user-role relationships for a specific user
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of user-role relationships for the specified user</returns>
        Task<IEnumerable<User_Role>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all user-role relationships for a specific role
        /// </summary>
        /// <param name="roleId">The role identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of user-role relationships for the specified role</returns>
        Task<IEnumerable<User_Role>> GetByRoleIdAsync(Guid roleId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific user-role relationship
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="roleId">The role identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The user-role relationship if found, otherwise null</returns>
        Task<User_Role?> GetByUserAndRoleIdAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a relationship exists between a user and a role
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="roleId">The role identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        Task<bool> ExistsByUserAndRoleAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a specific user-role relationship
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="roleId">The role identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        Task<bool> DeleteByUserAndRoleIdAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all user-role relationships for a specific user
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all user-role relationships for a specific role
        /// </summary>
        /// <param name="roleId">The role identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByRoleIdAsync(Guid roleId, CancellationToken cancellationToken = default);
    }
}
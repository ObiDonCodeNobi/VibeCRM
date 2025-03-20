using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Business
{
    /// <summary>
    /// Repository interface for managing User entities
    /// </summary>
    public interface IUserRepository : IRepository<User, Guid>
    {
        /// <summary>
        /// Gets a user by their username
        /// </summary>
        /// <param name="username">The username of the user</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The user if found, otherwise null</returns>
        Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a user by their email address
        /// </summary>
        /// <param name="email">The email address of the user</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The user if found, otherwise null</returns>
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all users that are members of a specific team
        /// </summary>
        /// <param name="teamId">The unique identifier of the team</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of users that are members of the specified team</returns>
        Task<IEnumerable<User>> GetByTeamIdAsync(Guid teamId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all users that have a specific role
        /// </summary>
        /// <param name="roleId">The unique identifier of the role</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of users that have the specified role</returns>
        Task<IEnumerable<User>> GetByRoleIdAsync(Guid roleId, CancellationToken cancellationToken = default);
    }
}
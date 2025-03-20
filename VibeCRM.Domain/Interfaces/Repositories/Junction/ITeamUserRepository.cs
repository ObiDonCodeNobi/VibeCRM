using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Team_User junction entities
    /// </summary>
    public interface ITeamUserRepository : IJunctionRepository<Team_User, Guid, Guid>
    {
        /// <summary>
        /// Gets all team-user relationships for a specific team
        /// </summary>
        /// <param name="teamId">The team identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of team-user relationships for the specified team</returns>
        Task<IEnumerable<Team_User>> GetByTeamIdAsync(Guid teamId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all team-user relationships for a specific user
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of team-user relationships for the specified user</returns>
        Task<IEnumerable<Team_User>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific team-user relationship
        /// </summary>
        /// <param name="teamId">The team identifier</param>
        /// <param name="userId">The user identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The team-user relationship if found, otherwise null</returns>
        Task<Team_User?> GetByTeamAndUserIdAsync(Guid teamId, Guid userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a relationship exists between a team and a user
        /// </summary>
        /// <param name="teamId">The team identifier</param>
        /// <param name="userId">The user identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship exists, otherwise false</returns>
        Task<bool> ExistsByTeamAndUserAsync(Guid teamId, Guid userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a specific team-user relationship
        /// </summary>
        /// <param name="teamId">The team identifier</param>
        /// <param name="userId">The user identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationship was successfully deleted, otherwise false</returns>
        Task<bool> DeleteByTeamAndUserIdAsync(Guid teamId, Guid userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all team-user relationships for a specific team
        /// </summary>
        /// <param name="teamId">The team identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByTeamIdAsync(Guid teamId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all team-user relationships for a specific user
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
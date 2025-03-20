using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Business
{
    /// <summary>
    /// Repository interface for managing Team entities
    /// </summary>
    public interface ITeamRepository : IRepository<Team, Guid>
    {
        /// <summary>
        /// Gets a team by its name
        /// </summary>
        /// <param name="name">The name of the team</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The team if found, otherwise null</returns>
        Task<Team?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all teams that a specific user is a member of
        /// </summary>
        /// <param name="userId">The unique identifier of the user</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of teams the specified user is a member of</returns>
        Task<IEnumerable<Team>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
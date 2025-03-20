using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.TypeStatus
{
    /// <summary>
    /// Repository interface for managing State entities
    /// </summary>
    public interface IStateRepository : IRepository<State, Guid>
    {
        /// <summary>
        /// Gets states ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of states ordered by their ordinal position</returns>
        Task<IEnumerable<State>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets states by name
        /// </summary>
        /// <param name="name">The state name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of states with the specified name</returns>
        Task<IEnumerable<State>> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets states by abbreviation
        /// </summary>
        /// <param name="abbreviation">The state abbreviation to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of states with the specified abbreviation</returns>
        Task<IEnumerable<State>> GetByAbbreviationAsync(string abbreviation, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the default state
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default state, or null if no states exist</returns>
        Task<State?> GetDefaultAsync(CancellationToken cancellationToken = default);
    }
}
using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.TypeStatus
{
    /// <summary>
    /// Repository interface for accessing and managing call direction entities
    /// </summary>
    public interface ICallDirectionRepository : IRepository<CallDirection, Guid>
    {
        /// <summary>
        /// Gets call directions ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of call directions ordered by their ordinal position</returns>
        Task<IEnumerable<CallDirection>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets call directions by direction name
        /// </summary>
        /// <param name="direction">The direction name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of call directions with the specified direction name</returns>
        Task<IEnumerable<CallDirection>> GetByDirectionAsync(string direction, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the default call direction
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default call direction, or null if no call directions exist</returns>
        Task<CallDirection?> GetDefaultAsync(CancellationToken cancellationToken = default);
    }
}
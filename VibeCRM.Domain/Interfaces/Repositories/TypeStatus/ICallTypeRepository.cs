using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.TypeStatus
{
    /// <summary>
    /// Repository interface for managing CallType entities
    /// </summary>
    public interface ICallTypeRepository : IRepository<CallType, Guid>
    {
        /// <summary>
        /// Gets call types ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of call types ordered by their ordinal position</returns>
        Task<IEnumerable<CallType>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets call types by type name
        /// </summary>
        /// <param name="type">The type name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of call types with the specified type name</returns>
        Task<IEnumerable<CallType>> GetByTypeAsync(string type, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the default call type (typically the one with the lowest ordinal position)
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default call type or null if not found</returns>
        Task<CallType?> GetDefaultAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets inbound call types
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of call types for inbound calls</returns>
        Task<IEnumerable<CallType>> GetInboundTypesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets outbound call types
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of call types for outbound calls</returns>
        Task<IEnumerable<CallType>> GetOutboundTypesAsync(CancellationToken cancellationToken = default);
    }
}
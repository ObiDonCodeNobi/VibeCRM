using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.TypeStatus
{
    /// <summary>
    /// Repository interface for managing AddressType entities
    /// </summary>
    public interface IAddressTypeRepository : IRepository<AddressType, Guid>
    {
        /// <summary>
        /// Gets address types ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of address types ordered by their ordinal position</returns>
        Task<IEnumerable<AddressType>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets address types by type name
        /// </summary>
        /// <param name="type">The type name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of address types with the specified type name</returns>
        Task<IEnumerable<AddressType>> GetByTypeAsync(string type, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the default address type (typically the one with the lowest ordinal position)
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default address type or null if not found</returns>
        Task<AddressType?> GetDefaultAsync(CancellationToken cancellationToken = default);
    }
}
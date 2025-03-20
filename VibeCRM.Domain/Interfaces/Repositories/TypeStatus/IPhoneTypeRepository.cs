using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.TypeStatus
{
    /// <summary>
    /// Repository interface for managing PhoneType entities
    /// </summary>
    public interface IPhoneTypeRepository : IRepository<PhoneType, Guid>
    {
        /// <summary>
        /// Gets phone types ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of phone types ordered by their ordinal position</returns>
        Task<IEnumerable<PhoneType>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets phone types by type name
        /// </summary>
        /// <param name="type">The type name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of phone types with the specified type name</returns>
        Task<IEnumerable<PhoneType>> GetByTypeAsync(string type, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the default phone type (typically the one with the lowest ordinal position)
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default phone type or null if not found</returns>
        Task<PhoneType?> GetDefaultAsync(CancellationToken cancellationToken = default);
    }
}
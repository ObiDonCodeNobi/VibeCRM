using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.TypeStatus
{
    /// <summary>
    /// Repository interface for managing EmailAddressType entities
    /// </summary>
    public interface IEmailAddressTypeRepository : IRepository<EmailAddressType, Guid>
    {
        /// <summary>
        /// Gets email address types ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of email address types ordered by their ordinal position</returns>
        Task<IEnumerable<EmailAddressType>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets email address types by type name
        /// </summary>
        /// <param name="type">The type name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of email address types with the specified type name</returns>
        Task<IEnumerable<EmailAddressType>> GetByTypeAsync(string type, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the default email address type (typically the one with the lowest ordinal position)
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default email address type or null if not found</returns>
        Task<EmailAddressType?> GetDefaultAsync(CancellationToken cancellationToken = default);
    }
}
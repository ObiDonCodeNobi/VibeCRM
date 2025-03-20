using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.TypeStatus
{
    /// <summary>
    /// Repository interface for managing AccountType entities
    /// </summary>
    public interface IAccountTypeRepository : IRepository<AccountType, Guid>
    {
        /// <summary>
        /// Gets account types ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of account types ordered by their ordinal position</returns>
        Task<IEnumerable<AccountType>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets account types by type name
        /// </summary>
        /// <param name="type">The type name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of account types with the specified type name</returns>
        Task<IEnumerable<AccountType>> GetByTypeAsync(string type, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the default account type
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default account type or null if not found</returns>
        Task<AccountType?> GetDefaultAsync(CancellationToken cancellationToken = default);
    }
}
using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.TypeStatus
{
    /// <summary>
    /// Repository interface for managing AccountStatus entities
    /// </summary>
    public interface IAccountStatusRepository : IRepository<AccountStatus, Guid>
    {
        /// <summary>
        /// Gets account statuses ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of account statuses ordered by their ordinal position</returns>
        Task<IEnumerable<AccountStatus>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets account statuses by status name
        /// </summary>
        /// <param name="status">The status name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of account statuses with the specified status name</returns>
        Task<IEnumerable<AccountStatus>> GetByStatusAsync(string status, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the default account status
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default account status, or null if no account statuses exist</returns>
        Task<AccountStatus?> GetDefaultAsync(CancellationToken cancellationToken = default);
    }
}
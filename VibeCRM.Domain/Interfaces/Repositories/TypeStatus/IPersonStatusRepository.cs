using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.TypeStatus
{
    /// <summary>
    /// Repository interface for managing PersonStatus entities
    /// </summary>
    public interface IPersonStatusRepository : IRepository<PersonStatus, Guid>
    {
        /// <summary>
        /// Gets person statuses ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person statuses ordered by their ordinal position</returns>
        Task<IEnumerable<PersonStatus>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets person statuses by status name
        /// </summary>
        /// <param name="status">The status name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person statuses with the specified status name</returns>
        Task<IEnumerable<PersonStatus>> GetByStatusAsync(string status, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the default person status
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default person status, or null if no person statuses exist</returns>
        Task<PersonStatus?> GetDefaultAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Soft deletes a person status by setting its Active flag to 0
        /// </summary>
        /// <param name="id">The unique identifier of the person status to delete</param>
        /// <param name="modifiedBy">The identifier of the user who is performing the delete operation</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the person status was deleted, false if not found</returns>
        Task<bool> DeleteAsync(Guid id, Guid modifiedBy, CancellationToken cancellationToken = default);
    }
}
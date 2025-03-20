using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.TypeStatus
{
    /// <summary>
    /// Repository interface for managing PersonType entities
    /// </summary>
    public interface IPersonTypeRepository : IRepository<PersonType, Guid>
    {
        /// <summary>
        /// Gets person types ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person types ordered by their ordinal position</returns>
        Task<IEnumerable<PersonType>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets person types by type name
        /// </summary>
        /// <param name="type">The type name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person types with the specified type name</returns>
        Task<IEnumerable<PersonType>> GetByTypeAsync(string type, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the default person type (typically the one with the lowest ordinal position)
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default person type or null if not found</returns>
        Task<PersonType?> GetDefaultAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a person type by its ID (soft delete) with the specified modifier
        /// </summary>
        /// <param name="id">The ID of the person type to delete</param>
        /// <param name="modifiedBy">The ID of the user who is performing the delete operation</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the person type was deleted successfully; otherwise, false</returns>
        Task<bool> DeleteAsync(Guid id, Guid modifiedBy, CancellationToken cancellationToken = default);
    }
}
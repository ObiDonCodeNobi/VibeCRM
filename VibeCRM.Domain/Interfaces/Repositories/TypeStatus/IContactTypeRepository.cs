using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.TypeStatus
{
    /// <summary>
    /// Repository interface for managing ContactType entities
    /// </summary>
    public interface IContactTypeRepository : IRepository<ContactType, Guid>
    {
        /// <summary>
        /// Gets contact types ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of contact types ordered by their ordinal position</returns>
        Task<IEnumerable<ContactType>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets contact types by type name
        /// </summary>
        /// <param name="type">The type name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of contact types with the specified type name</returns>
        Task<IEnumerable<ContactType>> GetByTypeAsync(string type, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the default contact type
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default contact type, or null if no contact types exist</returns>
        Task<ContactType?> GetDefaultAsync(CancellationToken cancellationToken = default);
    }
}
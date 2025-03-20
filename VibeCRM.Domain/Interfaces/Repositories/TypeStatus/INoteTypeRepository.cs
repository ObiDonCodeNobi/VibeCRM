using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.TypeStatus
{
    /// <summary>
    /// Repository interface for managing NoteType entities
    /// </summary>
    public interface INoteTypeRepository : IRepository<NoteType, Guid>
    {
        /// <summary>
        /// Gets note types ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of note types ordered by their ordinal position</returns>
        Task<IEnumerable<NoteType>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets note types by type name
        /// </summary>
        /// <param name="type">The type name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of note types with the specified type name</returns>
        Task<IEnumerable<NoteType>> GetByTypeAsync(string type, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the default note type (typically the one with the lowest ordinal position)
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default note type or null if not found</returns>
        Task<NoteType?> GetDefaultAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets note types by color code
        /// </summary>
        /// <param name="colorCode">The color code to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of note types with the specified color code</returns>
        Task<IEnumerable<NoteType>> GetByColorCodeAsync(string colorCode, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets note types by icon name
        /// </summary>
        /// <param name="iconName">The icon name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of note types with the specified icon name</returns>
        Task<IEnumerable<NoteType>> GetByIconNameAsync(string iconName, CancellationToken cancellationToken = default);
    }
}
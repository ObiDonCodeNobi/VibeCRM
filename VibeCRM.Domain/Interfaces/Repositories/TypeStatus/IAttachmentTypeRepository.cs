using VibeCRM.Domain.Entities.TypeStatusEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.TypeStatus
{
    /// <summary>
    /// Repository interface for managing AttachmentType entities
    /// </summary>
    public interface IAttachmentTypeRepository : IRepository<AttachmentType, Guid>
    {
        /// <summary>
        /// Gets attachment types ordered by their ordinal position
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of attachment types ordered by their ordinal position</returns>
        Task<IEnumerable<AttachmentType>> GetByOrdinalPositionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets attachment types by type name
        /// </summary>
        /// <param name="type">The type name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of attachment types with the specified type name</returns>
        Task<IEnumerable<AttachmentType>> GetByTypeAsync(string type, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the default attachment type (typically the one with the lowest ordinal position)
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default attachment type or null if not found</returns>
        Task<AttachmentType?> GetDefaultAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets attachment types by allowed file extension
        /// </summary>
        /// <param name="fileExtension">The file extension to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of attachment types that allow the specified file extension</returns>
        Task<IEnumerable<AttachmentType>> GetByFileExtensionAsync(string fileExtension, CancellationToken cancellationToken = default);
    }
}
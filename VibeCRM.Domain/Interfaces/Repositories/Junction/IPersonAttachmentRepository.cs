using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Person_Attachment junction entities
    /// </summary>
    public interface IPersonAttachmentRepository : IJunctionRepository<Person_Attachment, Guid, Guid>
    {
        /// <summary>
        /// Gets all person-attachment relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-attachment relationships for the specified person</returns>
        Task<IEnumerable<Person_Attachment>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all person-attachment relationships for a specific attachment
        /// </summary>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-attachment relationships for the specified attachment</returns>
        Task<IEnumerable<Person_Attachment>> GetByAttachmentIdAsync(Guid attachmentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific person-attachment relationship
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The person-attachment relationship if found, otherwise null</returns>
        Task<Person_Attachment?> GetByPersonAndAttachmentIdAsync(Guid personId, Guid attachmentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all person-attachment relationships for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all person-attachment relationships for a specific attachment
        /// </summary>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByAttachmentIdAsync(Guid attachmentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets person-attachment relationships by attachment type
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="attachmentTypeId">The attachment type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of person-attachment relationships with the specified attachment type</returns>
        Task<IEnumerable<Person_Attachment>> GetByAttachmentTypeAsync(Guid personId, Guid attachmentTypeId, CancellationToken cancellationToken = default);
    }
}
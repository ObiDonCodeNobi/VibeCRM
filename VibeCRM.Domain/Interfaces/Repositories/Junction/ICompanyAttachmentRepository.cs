using VibeCRM.Domain.Entities.JunctionEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Junction
{
    /// <summary>
    /// Repository interface for managing Company_Attachment junction entities
    /// </summary>
    public interface ICompanyAttachmentRepository : IJunctionRepository<Company_Attachment, Guid, Guid>
    {
        /// <summary>
        /// Gets all company-attachment relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-attachment relationships for the specified company</returns>
        Task<IEnumerable<Company_Attachment>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all company-attachment relationships for a specific attachment
        /// </summary>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-attachment relationships for the specified attachment</returns>
        Task<IEnumerable<Company_Attachment>> GetByAttachmentIdAsync(Guid attachmentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a specific company-attachment relationship
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The company-attachment relationship if found, otherwise null</returns>
        Task<Company_Attachment?> GetByCompanyAndAttachmentIdAsync(Guid companyId, Guid attachmentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all company-attachment relationships for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all company-attachment relationships for a specific attachment
        /// </summary>
        /// <param name="attachmentId">The attachment identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the relationships were successfully deleted, otherwise false</returns>
        Task<bool> DeleteByAttachmentIdAsync(Guid attachmentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets company-attachment relationships by attachment type
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="attachmentTypeId">The attachment type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of company-attachment relationships with the specified attachment type</returns>
        Task<IEnumerable<Company_Attachment>> GetByAttachmentTypeAsync(Guid companyId, Guid attachmentTypeId, CancellationToken cancellationToken = default);
    }
}
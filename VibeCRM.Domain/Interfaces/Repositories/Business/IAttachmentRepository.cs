using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Interfaces.Repositories.Business
{
    /// <summary>
    /// Repository interface for managing Attachment entities
    /// </summary>
    public interface IAttachmentRepository : IRepository<Attachment, Guid>
    {
        /// <summary>
        /// Gets attachments by their type
        /// </summary>
        /// <param name="attachmentTypeId">The attachment type identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of attachments with the specified type</returns>
        Task<IEnumerable<Attachment>> GetByAttachmentTypeAsync(Guid attachmentTypeId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets attachments for a specific company
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of attachments associated with the specified company</returns>
        Task<IEnumerable<Attachment>> GetByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets attachments for a specific person
        /// </summary>
        /// <param name="personId">The person identifier</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of attachments associated with the specified person</returns>
        Task<IEnumerable<Attachment>> GetByPersonAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets attachments by file name
        /// </summary>
        /// <param name="fileName">The file name to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of attachments with the specified file name</returns>
        Task<IEnumerable<Attachment>> GetByFileNameAsync(string fileName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets attachments by file extension
        /// </summary>
        /// <param name="fileExtension">The file extension to search for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of attachments with the specified file extension</returns>
        Task<IEnumerable<Attachment>> GetByFileExtensionAsync(string fileExtension, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets attachments created within a date range
        /// </summary>
        /// <param name="startDate">The start date of the range</param>
        /// <param name="endDate">The end date of the range</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of attachments created within the specified date range</returns>
        Task<IEnumerable<Attachment>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    }
}
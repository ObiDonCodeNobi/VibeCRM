using MediatR;
using VibeCRM.Application.Features.AttachmentType.DTOs;

namespace VibeCRM.Application.Features.AttachmentType.Queries.GetAttachmentTypeByFileExtension
{
    /// <summary>
    /// Query to retrieve attachment types that support a specific file extension.
    /// </summary>
    public class GetAttachmentTypeByFileExtensionQuery : IRequest<IEnumerable<AttachmentTypeListDto>>
    {
        /// <summary>
        /// Gets or sets the file extension to search for (e.g., ".pdf", ".docx").
        /// </summary>
        public string FileExtension { get; set; } = string.Empty;
    }
}
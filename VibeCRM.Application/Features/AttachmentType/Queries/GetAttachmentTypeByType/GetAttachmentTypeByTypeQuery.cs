using MediatR;
using VibeCRM.Shared.DTOs.AttachmentType;

namespace VibeCRM.Application.Features.AttachmentType.Queries.GetAttachmentTypeByType
{
    /// <summary>
    /// Query to retrieve an attachment type by its type name.
    /// </summary>
    public class GetAttachmentTypeByTypeQuery : IRequest<AttachmentTypeDetailsDto>
    {
        /// <summary>
        /// Gets or sets the type name of the attachment type to retrieve.
        /// </summary>
        public string Type { get; set; } = string.Empty;
    }
}
using MediatR;
using VibeCRM.Application.Features.Attachment.DTOs;

namespace VibeCRM.Application.Features.Attachment.Queries.GetAllAttachments
{
    /// <summary>
    /// Query to retrieve all attachments with pagination support.
    /// This is used in the CQRS pattern as the request object for fetching attachments.
    /// </summary>
    public class GetAllAttachmentsQuery : IRequest<List<AttachmentListDto>>
    {
        /// <summary>
        /// Gets or sets the page number for pagination (1-based)
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Gets or sets the page size for pagination
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
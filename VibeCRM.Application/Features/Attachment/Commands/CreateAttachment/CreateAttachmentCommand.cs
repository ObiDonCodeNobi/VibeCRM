using MediatR;
using VibeCRM.Shared.DTOs.Attachment;

namespace VibeCRM.Application.Features.Attachment.Commands.CreateAttachment
{
    /// <summary>
    /// Command to create a new attachment in the system.
    /// This is used in the CQRS pattern as the request object for attachment creation.
    /// </summary>
    public class CreateAttachmentCommand : IRequest<AttachmentDetailsDto>
    {
        /// <summary>
        /// Gets or sets the identifier of the attachment type.
        /// </summary>
        public Guid AttachmentTypeId { get; set; }

        /// <summary>
        /// Gets or sets the subject of the attachment.
        /// </summary>
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the file path or storage identifier of the attachment.
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the identifier of the user creating this attachment.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user modifying this attachment.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}
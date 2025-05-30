using MediatR;

namespace VibeCRM.Application.Features.AttachmentType.Commands.CreateAttachmentType
{
    /// <summary>
    /// Command for creating a new attachment type.
    /// </summary>
    public class CreateAttachmentTypeCommand : IRequest<Guid>
    {
        /// <summary>
        /// Gets or sets the attachment type name (e.g., "Document", "Image", "Contract").
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the attachment type with details about when this attachment type should be used.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting attachment types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the user who is creating the attachment type.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;
    }
}
namespace VibeCRM.Application.Features.Attachment.DTOs
{
    /// <summary>
    /// Detailed Data Transfer Object for Attachment entities.
    /// Contains all properties needed for detailed views and operations.
    /// </summary>
    public class AttachmentDetailsDto : AttachmentDto
    {
        /// <summary>
        /// Gets or sets the name of the attachment type.
        /// </summary>
        public string AttachmentTypeName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the identifier of the user who created this attachment.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when this attachment was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified this attachment.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when this attachment was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}
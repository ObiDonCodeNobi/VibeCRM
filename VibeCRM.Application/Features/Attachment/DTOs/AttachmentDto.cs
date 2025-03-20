namespace VibeCRM.Application.Features.Attachment.DTOs
{
    /// <summary>
    /// Base Data Transfer Object for Attachment entities.
    /// Contains common properties needed for basic operations.
    /// </summary>
    public class AttachmentDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the attachment.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the attachment type identifier.
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
        /// Gets or sets the filename extracted from the path.
        /// </summary>
        public string Filename { get; set; } = string.Empty;
    }
}
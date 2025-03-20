namespace VibeCRM.Application.Features.AttachmentType.DTOs
{
    /// <summary>
    /// Data Transfer Object for attachment type list information.
    /// Contains the basic properties of an attachment type along with attachment count.
    /// Used for displaying attachment types in list views.
    /// </summary>
    public class AttachmentTypeListDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the attachment type.
        /// </summary>
        public Guid Id { get; set; }

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
        /// Gets or sets the number of attachments associated with this attachment type.
        /// </summary>
        public int AttachmentCount { get; set; }
    }
}
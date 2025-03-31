namespace VibeCRM.Shared.DTOs.AttachmentType
{
    /// <summary>
    /// Data Transfer Object for detailed attachment type information.
    /// Contains all properties of an attachment type including audit fields.
    /// Used for displaying detailed attachment type information.
    /// </summary>
    public class AttachmentTypeDetailsDto
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

        /// <summary>
        /// Gets or sets a value indicating whether the attachment type is active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the attachment type was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the user who created the attachment type.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the attachment type was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the user who last modified the attachment type.
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;
    }
}
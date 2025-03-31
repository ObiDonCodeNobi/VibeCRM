namespace VibeCRM.Shared.DTOs.AttachmentType
{
    /// <summary>
    /// Data Transfer Object for attachment type information.
    /// Contains the basic properties of an attachment type.
    /// </summary>
    public class AttachmentTypeDto
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
    }
}
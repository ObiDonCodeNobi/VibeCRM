namespace VibeCRM.Shared.DTOs.EmailAddressType
{
    /// <summary>
    /// Data Transfer Object for email address type information.
    /// Contains the basic properties of an email address type.
    /// </summary>
    public class EmailAddressTypeDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the email address type.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the email address type name (e.g., "Personal", "Work").
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address type description with details about when this email address type should be used.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting email address types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}
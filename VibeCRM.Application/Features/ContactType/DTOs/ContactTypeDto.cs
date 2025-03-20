namespace VibeCRM.Application.Features.ContactType.DTOs
{
    /// <summary>
    /// Data Transfer Object for contact type information.
    /// Contains the basic properties of a contact type.
    /// </summary>
    public class ContactTypeDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the contact type.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the contact type name (e.g., "Customer", "Vendor", "Partner").
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the contact type description with details about when this contact type should be used.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting contact types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}
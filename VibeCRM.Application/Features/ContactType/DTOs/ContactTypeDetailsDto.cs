namespace VibeCRM.Application.Features.ContactType.DTOs
{
    /// <summary>
    /// Data Transfer Object for detailed contact type information.
    /// Contains the basic properties of a contact type along with audit information and contact count.
    /// Used for displaying detailed contact type information.
    /// </summary>
    public class ContactTypeDetailsDto
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

        /// <summary>
        /// Gets or sets the number of contacts associated with this contact type.
        /// </summary>
        public int ContactCount { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created this contact type.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when this contact type was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified this contact type.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when this contact type was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this contact type is active.
        /// </summary>
        public bool Active { get; set; }
    }
}
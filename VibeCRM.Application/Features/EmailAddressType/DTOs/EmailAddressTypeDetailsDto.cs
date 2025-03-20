namespace VibeCRM.Application.Features.EmailAddressType.DTOs
{
    /// <summary>
    /// Data Transfer Object for detailed email address type information.
    /// Contains the basic properties of an email address type along with audit information and email address count.
    /// Used for displaying detailed email address type information.
    /// </summary>
    public class EmailAddressTypeDetailsDto
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

        /// <summary>
        /// Gets or sets the number of email addresses associated with this email address type.
        /// </summary>
        public int EmailAddressCount { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created this email address type.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when this email address type was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified this email address type.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when this email address type was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this email address type is active.
        /// </summary>
        public bool Active { get; set; }
    }
}
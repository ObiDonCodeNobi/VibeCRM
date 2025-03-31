namespace VibeCRM.Shared.DTOs.PhoneType
{
    /// <summary>
    /// Data Transfer Object for detailed phone type information.
    /// Contains the complete properties of a phone type with additional metadata.
    /// </summary>
    public class PhoneTypeDetailsDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the phone type.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the phone type name (e.g., "Home", "Work", "Mobile", "Fax").
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone type description with details about when this phone type should be used.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting phone types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the count of phones using this phone type.
        /// </summary>
        public int PhoneCount { get; set; }

        /// <summary>
        /// Gets or sets the date when the phone type was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created the phone type.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date when the phone type was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the phone type.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the phone type is active.
        /// </summary>
        public bool Active { get; set; }
    }
}
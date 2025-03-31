namespace VibeCRM.Shared.DTOs.PhoneType
{
    /// <summary>
    /// Data Transfer Object for phone type list information.
    /// Contains the basic properties of a phone type with additional information for list views.
    /// </summary>
    public class PhoneTypeListDto
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
    }
}
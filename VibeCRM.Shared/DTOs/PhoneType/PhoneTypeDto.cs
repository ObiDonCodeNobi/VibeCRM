namespace VibeCRM.Shared.DTOs.PhoneType
{
    /// <summary>
    /// Data Transfer Object for phone type information.
    /// Contains the basic properties of a phone type.
    /// </summary>
    public class PhoneTypeDto
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
    }
}
namespace VibeCRM.Shared.DTOs.PersonType
{
    /// <summary>
    /// Data Transfer Object for basic PersonType information.
    /// Used for transferring person type data between application layers.
    /// </summary>
    public class PersonTypeDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the person type.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the type name (e.g., "Customer", "Vendor", "Employee").
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the person type.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting person types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}
namespace VibeCRM.Shared.DTOs.PersonType
{
    /// <summary>
    /// Data Transfer Object for detailed PersonType information.
    /// Includes audit information and additional details about the person type.
    /// </summary>
    public class PersonTypeDetailsDto
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

        /// <summary>
        /// Gets or sets the date when the person type was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created the person type.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date when the person type was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the person type.
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of people currently assigned to this type.
        /// </summary>
        public int PeopleCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the person type is active.
        /// </summary>
        public bool Active { get; set; }
    }
}
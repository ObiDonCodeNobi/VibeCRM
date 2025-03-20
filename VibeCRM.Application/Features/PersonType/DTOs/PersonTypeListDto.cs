namespace VibeCRM.Application.Features.PersonType.DTOs
{
    /// <summary>
    /// Data Transfer Object for PersonType list view.
    /// Used for displaying person types in list views and dropdowns.
    /// </summary>
    public class PersonTypeListDto
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
        /// Gets or sets the number of people currently assigned to this type.
        /// </summary>
        public int PeopleCount { get; set; }
    }
}
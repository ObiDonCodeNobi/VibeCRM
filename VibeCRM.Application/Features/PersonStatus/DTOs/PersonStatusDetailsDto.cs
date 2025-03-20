namespace VibeCRM.Application.Features.PersonStatus.DTOs
{
    /// <summary>
    /// Data Transfer Object for detailed PersonStatus information.
    /// Includes audit information and additional details about the person status.
    /// </summary>
    public class PersonStatusDetailsDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the person status.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the status name (e.g., "Active", "Inactive", "Prospect").
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the person status.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting person statuses in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the date when the person status was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created the person status.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date when the person status was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the person status.
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of people currently assigned to this status.
        /// </summary>
        public int PeopleCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the person status is active.
        /// </summary>
        public bool Active { get; set; }
    }
}
namespace VibeCRM.Application.Features.ActivityType.DTOs
{
    /// <summary>
    /// Data Transfer Object for detailed ActivityType information.
    /// Contains the core properties of an activity type along with related data for detailed views.
    /// </summary>
    public class ActivityTypeDetailsDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the activity type.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the type name (e.g., "Meeting", "Call", "Email").
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the activity type.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting activity types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the count of activities with this type.
        /// </summary>
        public int ActivityCount { get; set; }

        /// <summary>
        /// Gets or sets the date when the activity type was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the user who created the activity type.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date when the activity type was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the user who last modified the activity type.
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;
    }
}
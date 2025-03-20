namespace VibeCRM.Application.Features.ActivityType.DTOs
{
    /// <summary>
    /// Data Transfer Object for ActivityType list views.
    /// Contains the core properties of an activity type along with additional information for list displays.
    /// </summary>
    public class ActivityTypeListDto
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
    }
}
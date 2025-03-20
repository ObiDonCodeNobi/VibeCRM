namespace VibeCRM.Application.Features.ActivityType.DTOs
{
    /// <summary>
    /// Data Transfer Object for basic ActivityType information.
    /// Contains the core properties of an activity type.
    /// </summary>
    public class ActivityTypeDto
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
    }
}
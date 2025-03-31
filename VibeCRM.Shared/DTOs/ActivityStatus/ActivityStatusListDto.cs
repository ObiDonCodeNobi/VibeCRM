namespace VibeCRM.Shared.DTOs.ActivityStatus
{
    /// <summary>
    /// Data Transfer Object for ActivityStatus list views.
    /// Contains the core properties of an activity status along with additional information for list displays.
    /// </summary>
    public class ActivityStatusListDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the activity status.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the status name (e.g., "Scheduled", "In Progress", "Completed").
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the activity status.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting activity statuses in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the count of activities with this status.
        /// </summary>
        public int ActivityCount { get; set; }
    }
}
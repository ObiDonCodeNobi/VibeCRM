namespace VibeCRM.Application.Features.Activity.DTOs
{
    /// <summary>
    /// List Data Transfer Object for Activity entities.
    /// Contains properties needed for displaying activities in list views.
    /// </summary>
    public class ActivityListDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the activity.
        /// </summary>
        public Guid ActivityId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the activity type.
        /// </summary>
        public Guid ActivityTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the activity type.
        /// </summary>
        public string ActivityTypeName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the identifier of the activity status.
        /// </summary>
        public Guid ActivityStatusId { get; set; }

        /// <summary>
        /// Gets or sets the name of the activity status.
        /// </summary>
        public string ActivityStatusName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the subject of the activity.
        /// </summary>
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the activity.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the due date of the activity.
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Gets or sets the start date of the activity.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets whether this activity is completed.
        /// </summary>
        public bool IsCompleted { get; set; }
    }
}
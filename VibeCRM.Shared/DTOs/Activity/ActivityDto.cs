namespace VibeCRM.Shared.DTOs.Activity
{
    /// <summary>
    /// Base Data Transfer Object for Activity entities.
    /// Contains the essential properties needed for basic operations.
    /// </summary>
    public class ActivityDto
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
        /// Gets or sets the identifier of the activity status.
        /// </summary>
        public Guid ActivityStatusId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user assigned to this activity.
        /// </summary>
        public Guid? AssignedUserId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the team assigned to this activity.
        /// </summary>
        public Guid? AssignedTeamId { get; set; }

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
        /// Gets or sets the date when the activity was completed.
        /// </summary>
        public DateTime? CompletedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who completed this activity.
        /// </summary>
        public Guid? CompletedBy { get; set; }

        /// <summary>
        /// Gets or sets whether this activity is completed.
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Gets or sets whether this activity is active (not soft-deleted).
        /// When true, the activity is active and visible in queries.
        /// When false, the activity is considered deleted but remains in the database.
        /// </summary>
        public bool Active { get; set; } = true;
    }
}
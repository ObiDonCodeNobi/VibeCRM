namespace VibeCRM.Application.Features.ActivityDefinition.DTOs
{
    /// <summary>
    /// Detailed Data Transfer Object for ActivityDefinition entities.
    /// Contains additional properties for detailed views and operations.
    /// </summary>
    public class ActivityDefinitionDetailsDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the activity definition.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the activity type.
        /// </summary>
        public Guid ActivityTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the activity type.
        /// </summary>
        public string ActivityTypeName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unique identifier of the activity status.
        /// </summary>
        public Guid ActivityStatusId { get; set; }

        /// <summary>
        /// Gets or sets the name of the activity status.
        /// </summary>
        public string ActivityStatusName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unique identifier of the user assigned to this activity definition.
        /// </summary>
        public Guid AssignedUserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the assigned user.
        /// </summary>
        public string AssignedUserName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unique identifier of the team assigned to this activity definition.
        /// </summary>
        public Guid AssignedTeamId { get; set; }

        /// <summary>
        /// Gets or sets the name of the assigned team.
        /// </summary>
        public string AssignedTeamName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the subject or title of the activity definition.
        /// </summary>
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the detailed description of the activity definition.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of days from the creation date when the activity will be due.
        /// </summary>
        public int DueDateOffset { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user who created this activity definition.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the name of the user who created this activity definition.
        /// </summary>
        public string CreatedByName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when this activity definition was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user who last modified this activity definition.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the name of the user who last modified this activity definition.
        /// </summary>
        public string ModifiedByName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when this activity definition was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets whether this activity definition is active (not soft-deleted).
        /// When true, the activity definition is active and visible in queries.
        /// When false, the activity definition is considered deleted but remains in the database.
        /// </summary>
        public bool Active { get; set; } = true;
    }
}
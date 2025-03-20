namespace VibeCRM.Application.Features.ActivityDefinition.DTOs
{
    /// <summary>
    /// Base Data Transfer Object for ActivityDefinition entities.
    /// Contains the essential properties needed for basic operations.
    /// </summary>
    public class ActivityDefinitionDto
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
        /// Gets or sets the unique identifier of the activity status.
        /// </summary>
        public Guid ActivityStatusId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user assigned to this activity definition.
        /// </summary>
        public Guid AssignedUserId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the team assigned to this activity definition.
        /// </summary>
        public Guid AssignedTeamId { get; set; }

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
        /// Gets or sets whether this activity definition is active (not soft-deleted).
        /// When true, the activity definition is active and visible in queries.
        /// When false, the activity definition is considered deleted but remains in the database.
        /// </summary>
        public bool Active { get; set; } = true;
    }
}
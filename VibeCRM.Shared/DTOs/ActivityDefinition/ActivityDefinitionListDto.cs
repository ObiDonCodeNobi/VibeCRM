namespace VibeCRM.Shared.DTOs.ActivityDefinition
{
    /// <summary>
    /// List Data Transfer Object for ActivityDefinition entities.
    /// Contains properties needed for displaying activity definitions in lists.
    /// </summary>
    public class ActivityDefinitionListDto
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
        /// Gets or sets the subject or title of the activity definition.
        /// </summary>
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of days from the creation date when the activity will be due.
        /// </summary>
        public int DueDateOffset { get; set; }

        /// <summary>
        /// Gets or sets the name of the assigned user.
        /// </summary>
        public string AssignedUserName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the assigned team.
        /// </summary>
        public string AssignedTeamName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets whether this activity definition is active (not soft-deleted).
        /// </summary>
        public bool Active { get; set; } = true;
    }
}
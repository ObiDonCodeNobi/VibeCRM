namespace VibeCRM.Application.Features.Activity.DTOs
{
    /// <summary>
    /// Detailed Data Transfer Object for Activity entities.
    /// Contains all properties needed for detailed views and operations.
    /// </summary>
    public class ActivityDetailsDto : ActivityDto
    {
        /// <summary>
        /// Gets or sets the name of the activity type.
        /// </summary>
        public string ActivityTypeName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the activity status.
        /// </summary>
        public string ActivityStatusName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the assigned user, if any.
        /// </summary>
        public string? AssignedUserName { get; set; }

        /// <summary>
        /// Gets or sets the name of the assigned team, if any.
        /// </summary>
        public string? AssignedTeamName { get; set; }

        /// <summary>
        /// Gets or sets the name of the user who completed this activity, if any.
        /// </summary>
        public string? CompletedByName { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created this activity.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when this activity was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified this activity.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when this activity was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}
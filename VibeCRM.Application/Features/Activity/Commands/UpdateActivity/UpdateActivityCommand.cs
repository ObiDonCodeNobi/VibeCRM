using MediatR;
using VibeCRM.Shared.DTOs.Activity;

namespace VibeCRM.Application.Features.Activity.Commands.UpdateActivity
{
    /// <summary>
    /// Command to update an existing activity in the system.
    /// This is used in the CQRS pattern as the request object for activity updates.
    /// </summary>
    public class UpdateActivityCommand : IRequest<ActivityDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the activity to update.
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
        /// Gets or sets whether this activity is completed.
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Gets or sets the date when the activity was completed.
        /// </summary>
        public DateTime? CompletedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who completed this activity.
        /// </summary>
        public Guid? CompletedBy { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user modifying this activity.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}
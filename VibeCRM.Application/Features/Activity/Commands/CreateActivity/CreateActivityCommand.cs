using MediatR;
using VibeCRM.Application.Features.Activity.DTOs;

namespace VibeCRM.Application.Features.Activity.Commands.CreateActivity
{
    /// <summary>
    /// Command to create a new activity in the system.
    /// This is used in the CQRS pattern as the request object for activity creation.
    /// </summary>
    public class CreateActivityCommand : IRequest<ActivityDetailsDto>
    {
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
        /// Gets or sets the identifier of the user creating this activity.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user modifying this activity.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}
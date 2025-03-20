using MediatR;
using VibeCRM.Application.Features.ActivityDefinition.DTOs;

namespace VibeCRM.Application.Features.ActivityDefinition.Commands.CreateActivityDefinition
{
    /// <summary>
    /// Command for creating a new activity definition.
    /// Implements the CQRS command pattern for creating activity definition entities.
    /// </summary>
    public class CreateActivityDefinitionCommand : IRequest<ActivityDefinitionDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the activity definition.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

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
        /// Gets or sets the unique identifier of the user who created this activity definition.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user who last modified this activity definition.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}
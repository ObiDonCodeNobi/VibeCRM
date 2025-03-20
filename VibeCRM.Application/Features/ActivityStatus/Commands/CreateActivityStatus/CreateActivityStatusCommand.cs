using MediatR;

namespace VibeCRM.Application.Features.ActivityStatus.Commands.CreateActivityStatus
{
    /// <summary>
    /// Command for creating a new activity status.
    /// </summary>
    public class CreateActivityStatusCommand : IRequest<Guid>
    {
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
    }
}
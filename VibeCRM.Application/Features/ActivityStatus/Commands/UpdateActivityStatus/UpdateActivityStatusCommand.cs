using MediatR;

namespace VibeCRM.Application.Features.ActivityStatus.Commands.UpdateActivityStatus
{
    /// <summary>
    /// Command for updating an existing activity status.
    /// </summary>
    public class UpdateActivityStatusCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the activity status to update.
        /// </summary>
        public Guid Id { get; set; }

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
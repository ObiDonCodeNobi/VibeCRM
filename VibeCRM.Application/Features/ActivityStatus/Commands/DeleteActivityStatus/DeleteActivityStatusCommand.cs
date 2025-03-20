using MediatR;

namespace VibeCRM.Application.Features.ActivityStatus.Commands.DeleteActivityStatus
{
    /// <summary>
    /// Command for deleting (soft delete) an activity status.
    /// </summary>
    public class DeleteActivityStatusCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the activity status to delete.
        /// </summary>
        public Guid Id { get; set; }
    }
}
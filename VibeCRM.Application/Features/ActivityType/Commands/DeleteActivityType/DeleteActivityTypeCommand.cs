using MediatR;

namespace VibeCRM.Application.Features.ActivityType.Commands.DeleteActivityType
{
    /// <summary>
    /// Command for deleting (soft delete) an activity type.
    /// </summary>
    public class DeleteActivityTypeCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the activity type to delete.
        /// </summary>
        public Guid Id { get; set; }
    }
}
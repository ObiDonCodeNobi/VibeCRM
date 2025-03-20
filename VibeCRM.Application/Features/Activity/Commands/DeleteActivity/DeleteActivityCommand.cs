using MediatR;

namespace VibeCRM.Application.Features.Activity.Commands.DeleteActivity
{
    /// <summary>
    /// Command to soft delete an existing activity in the system.
    /// This is used in the CQRS pattern as the request object for activity soft deletion.
    /// The soft delete operation sets the Active property to false rather than removing the record.
    /// </summary>
    public class DeleteActivityCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the activity to soft delete.
        /// </summary>
        public Guid ActivityId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who is performing the soft delete operation.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}
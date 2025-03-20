using MediatR;

namespace VibeCRM.Application.Features.PersonStatus.Commands.DeletePersonStatus
{
    /// <summary>
    /// Command for soft deleting a person status.
    /// Implements the CQRS command pattern for soft deleting a person status entity.
    /// </summary>
    public class DeletePersonStatusCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the person status to delete.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who is performing the delete operation.
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;
    }
}
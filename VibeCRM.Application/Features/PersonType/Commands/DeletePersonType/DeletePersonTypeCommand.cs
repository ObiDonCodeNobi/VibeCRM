using MediatR;

namespace VibeCRM.Application.Features.PersonType.Commands.DeletePersonType
{
    /// <summary>
    /// Command for deleting an existing person type.
    /// Implements the CQRS command pattern for soft-deleting a person type entity.
    /// </summary>
    public class DeletePersonTypeCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the person type to delete.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who is performing the delete operation.
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;
    }
}
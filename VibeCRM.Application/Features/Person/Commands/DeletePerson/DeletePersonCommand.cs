using MediatR;

namespace VibeCRM.Application.Features.Person.Commands.DeletePerson
{
    /// <summary>
    /// Command to delete (soft-delete) an existing person in the system.
    /// This is used in the CQRS pattern as the request object for person deletion.
    /// </summary>
    public class DeletePersonCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the person to delete.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user performing the deletion.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}
using MediatR;

namespace VibeCRM.Application.Features.Service.Commands.DeleteService
{
    /// <summary>
    /// Command to delete (soft delete) a service from the system.
    /// This is used in the CQRS pattern as the request object for service deletion.
    /// </summary>
    public class DeleteServiceCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the service to delete.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user performing the deletion.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteServiceCommand"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the service to delete.</param>
        /// <param name="modifiedBy">The identifier of the user performing the deletion.</param>
        public DeleteServiceCommand(Guid id, Guid modifiedBy)
        {
            Id = id;
            ModifiedBy = modifiedBy;
        }
    }
}
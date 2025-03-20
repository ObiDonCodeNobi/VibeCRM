using MediatR;

namespace VibeCRM.Application.Features.Call.Commands.DeleteCall
{
    /// <summary>
    /// Command for soft-deleting an existing call in the system.
    /// Implements the CQRS command pattern for call deletion.
    /// </summary>
    public class DeleteCallCommand : IRequest<bool>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCallCommand"/> class.
        /// </summary>
        public DeleteCallCommand()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCallCommand"/> class with specified parameters.
        /// </summary>
        /// <param name="id">The unique identifier of the call to delete.</param>
        /// <param name="modifiedBy">The ID of the user performing the deletion.</param>
        public DeleteCallCommand(Guid id, Guid modifiedBy)
        {
            Id = id;
            ModifiedBy = modifiedBy;
        }

        /// <summary>
        /// Gets or sets the unique identifier of the call to delete.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who is performing the deletion.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}
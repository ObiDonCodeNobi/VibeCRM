using MediatR;

namespace VibeCRM.Application.Features.CallType.Commands.DeleteCallType
{
    /// <summary>
    /// Command for soft deleting an existing call type in the system.
    /// Implements IRequest to return a boolean indicating success.
    /// </summary>
    public class DeleteCallTypeCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the call type to delete.
        /// </summary>
        public Guid Id { get; set; }
    }
}
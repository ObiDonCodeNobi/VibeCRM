using System;
using MediatR;

namespace VibeCRM.Application.Features.State.Commands.DeleteState
{
    /// <summary>
    /// Command for deleting an existing state.
    /// </summary>
    public class DeleteStateCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the state to delete.
        /// </summary>
        public Guid Id { get; set; }
    }
}

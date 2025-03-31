using MediatR;
using VibeCRM.Shared.DTOs.Call;

namespace VibeCRM.Application.Features.Call.Commands.UpdateCall
{
    /// <summary>
    /// Command for updating an existing call in the system.
    /// Implements the CQRS command pattern for call updates.
    /// </summary>
    public class UpdateCallCommand : IRequest<CallDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the call to update.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the call type identifier.
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// Gets or sets the call status identifier.
        /// </summary>
        public Guid StatusId { get; set; }

        /// <summary>
        /// Gets or sets the call direction identifier.
        /// </summary>
        public Guid DirectionId { get; set; }

        /// <summary>
        /// Gets or sets the description of the call.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the duration of the call in seconds.
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who last modified the call.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        public UpdateCallCommand()
        {
            Description = string.Empty;
        }
    }
}
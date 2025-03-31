using MediatR;
using VibeCRM.Shared.DTOs.Call;

namespace VibeCRM.Application.Features.Call.Commands.CreateCall
{
    /// <summary>
    /// Command for creating a new call in the system.
    /// Implements the CQRS command pattern for call creation.
    /// </summary>
    public class CreateCallCommand : IRequest<CallDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the call.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

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
        /// Gets or sets the ID of the user who created the call.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who last modified the call.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        public CreateCallCommand()
        { Description = string.Empty; }
    }
}
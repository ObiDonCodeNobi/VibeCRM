using MediatR;
using VibeCRM.Shared.DTOs.CallType;

namespace VibeCRM.Application.Features.CallType.Commands.CreateCallType
{
    /// <summary>
    /// Command for creating a new call type in the system.
    /// Implements IRequest to return a CallTypeDto after successful creation.
    /// </summary>
    public class CreateCallTypeCommand : IRequest<CallTypeDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the call type.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the type name (e.g., "Sales", "Support", "Follow-up").
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the call type with additional details.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting call types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}
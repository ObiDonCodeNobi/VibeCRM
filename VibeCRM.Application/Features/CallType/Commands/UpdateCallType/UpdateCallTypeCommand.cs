using MediatR;
using VibeCRM.Application.Features.CallType.DTOs;

namespace VibeCRM.Application.Features.CallType.Commands.UpdateCallType
{
    /// <summary>
    /// Command for updating an existing call type in the system.
    /// Implements IRequest to return a CallTypeDto after successful update.
    /// </summary>
    public class UpdateCallTypeCommand : IRequest<CallTypeDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the call type to update.
        /// </summary>
        public Guid Id { get; set; }

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
using MediatR;

namespace VibeCRM.Application.Features.CallDirection.Commands.UpdateCallDirection
{
    /// <summary>
    /// Command for updating an existing call direction.
    /// </summary>
    public class UpdateCallDirectionCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the call direction to update.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the direction name (e.g., "Inbound", "Outbound", "Missed").
        /// </summary>
        public string Direction { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the call direction with additional details.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting call directions in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who is updating the call direction.
        /// This will be stored as ModifiedBy in the database.
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;
    }
}
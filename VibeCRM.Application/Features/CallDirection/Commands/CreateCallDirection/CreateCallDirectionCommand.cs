using MediatR;

namespace VibeCRM.Application.Features.CallDirection.Commands.CreateCallDirection
{
    /// <summary>
    /// Command for creating a new call direction.
    /// </summary>
    public class CreateCallDirectionCommand : IRequest<Guid>
    {
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
        /// Gets or sets the identifier of the user who is creating the call direction.
        /// This will be stored as CreatedBy and ModifiedBy in the database.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;
    }
}
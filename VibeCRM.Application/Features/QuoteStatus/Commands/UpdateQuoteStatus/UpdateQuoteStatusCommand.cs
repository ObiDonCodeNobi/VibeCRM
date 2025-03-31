using MediatR;
using VibeCRM.Shared.DTOs.QuoteStatus;

namespace VibeCRM.Application.Features.QuoteStatus.Commands.UpdateQuoteStatus
{
    /// <summary>
    /// Command for updating an existing quote status.
    /// </summary>
    public class UpdateQuoteStatusCommand : IRequest<QuoteStatusDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the quote status to update.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the quote status (e.g., "Draft", "Sent", "Accepted", "Rejected").
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a detailed description of the quote status.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting quote statuses in listings.
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}
using MediatR;
using VibeCRM.Application.Features.QuoteStatus.DTOs;

namespace VibeCRM.Application.Features.QuoteStatus.Commands.CreateQuoteStatus
{
    /// <summary>
    /// Command for creating a new quote status.
    /// </summary>
    public class CreateQuoteStatusCommand : IRequest<QuoteStatusDto>
    {
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
using MediatR;
using VibeCRM.Application.Features.Quote.DTOs;

namespace VibeCRM.Application.Features.Quote.Commands.CreateQuote
{
    /// <summary>
    /// Command for creating a new quote
    /// </summary>
    public class CreateQuoteCommand : IRequest<QuoteDetailsDto>
    {
        /// <summary>
        /// Gets or sets the quote number
        /// </summary>
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quote status identifier
        /// </summary>
        public Guid? QuoteStatusId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created the quote
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the quote
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}
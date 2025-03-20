using MediatR;

namespace VibeCRM.Application.Features.QuoteLineItem.Queries.GetTotalForQuote
{
    /// <summary>
    /// Query to retrieve the total amount for a specific quote
    /// </summary>
    public class GetTotalForQuoteQuery : IRequest<decimal>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the quote to calculate the total for
        /// </summary>
        public Guid QuoteId { get; set; }
    }
}
using MediatR;
using VibeCRM.Application.Features.QuoteLineItem.DTOs;

namespace VibeCRM.Application.Features.QuoteLineItem.Queries.GetQuoteLineItemsByQuote
{
    /// <summary>
    /// Query to retrieve all quote line items for a specific quote
    /// </summary>
    public class GetQuoteLineItemsByQuoteQuery : IRequest<IEnumerable<QuoteLineItemDetailsDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the quote to retrieve line items for
        /// </summary>
        public Guid QuoteId { get; set; }
    }
}
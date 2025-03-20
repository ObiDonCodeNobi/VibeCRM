using MediatR;
using VibeCRM.Application.Features.Quote.DTOs;

namespace VibeCRM.Application.Features.Quote.Queries.GetQuotesByNumber
{
    /// <summary>
    /// Query for retrieving quotes by their number
    /// </summary>
    public class GetQuotesByNumberQuery : IRequest<IEnumerable<QuoteListDto>>
    {
        /// <summary>
        /// Gets or sets the quote number to search for
        /// </summary>
        public string Number { get; set; } = string.Empty;
    }
}
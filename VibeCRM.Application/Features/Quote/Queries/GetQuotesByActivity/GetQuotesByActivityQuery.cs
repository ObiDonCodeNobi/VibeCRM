using MediatR;
using VibeCRM.Application.Features.Quote.DTOs;

namespace VibeCRM.Application.Features.Quote.Queries.GetQuotesByActivity
{
    /// <summary>
    /// Query for retrieving quotes associated with a specific activity
    /// </summary>
    public class GetQuotesByActivityQuery : IRequest<IEnumerable<QuoteListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the activity to retrieve quotes for
        /// </summary>
        public Guid ActivityId { get; set; }
    }
}
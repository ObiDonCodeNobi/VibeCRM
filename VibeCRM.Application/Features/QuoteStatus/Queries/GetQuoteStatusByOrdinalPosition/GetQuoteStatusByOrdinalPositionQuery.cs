using MediatR;
using VibeCRM.Application.Features.QuoteStatus.DTOs;

namespace VibeCRM.Application.Features.QuoteStatus.Queries.GetQuoteStatusByOrdinalPosition
{
    /// <summary>
    /// Query for retrieving quote statuses by their ordinal position.
    /// </summary>
    public class GetQuoteStatusByOrdinalPositionQuery : IRequest<IEnumerable<QuoteStatusDto>>
    {
        /// <summary>
        /// Gets or sets the ordinal position to retrieve quote statuses by.
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}

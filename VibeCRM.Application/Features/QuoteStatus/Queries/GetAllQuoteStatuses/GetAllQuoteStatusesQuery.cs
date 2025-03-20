using MediatR;
using VibeCRM.Application.Features.QuoteStatus.DTOs;

namespace VibeCRM.Application.Features.QuoteStatus.Queries.GetAllQuoteStatuses
{
    /// <summary>
    /// Query for retrieving all quote statuses.
    /// </summary>
    public class GetAllQuoteStatusesQuery : IRequest<IEnumerable<QuoteStatusListDto>>
    {
        // No parameters needed for this query
    }
}

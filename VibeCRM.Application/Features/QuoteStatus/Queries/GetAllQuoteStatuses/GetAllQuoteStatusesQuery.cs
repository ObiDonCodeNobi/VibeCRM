using MediatR;
using VibeCRM.Shared.DTOs.QuoteStatus;

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
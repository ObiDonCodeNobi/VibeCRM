using MediatR;
using VibeCRM.Application.Features.QuoteStatus.DTOs;

namespace VibeCRM.Application.Features.QuoteStatus.Queries.GetDefaultQuoteStatus
{
    /// <summary>
    /// Query for retrieving the default quote status.
    /// The default quote status is typically the one with the lowest ordinal position.
    /// </summary>
    public class GetDefaultQuoteStatusQuery : IRequest<QuoteStatusDto>
    {
        // No parameters needed for this query
    }
}

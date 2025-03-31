using MediatR;
using VibeCRM.Shared.DTOs.Quote;

namespace VibeCRM.Application.Features.Quote.Queries.GetAllQuotes
{
    /// <summary>
    /// Query for retrieving all active quotes
    /// </summary>
    public class GetAllQuotesQuery : IRequest<IEnumerable<QuoteListDto>>
    {
        // No parameters needed for this query as it retrieves all active quotes
    }
}
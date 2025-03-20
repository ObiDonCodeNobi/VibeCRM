using MediatR;
using VibeCRM.Application.Features.QuoteLineItem.DTOs;

namespace VibeCRM.Application.Features.QuoteLineItem.Queries.GetAllQuoteLineItems
{
    /// <summary>
    /// Query to retrieve all active quote line items
    /// </summary>
    public class GetAllQuoteLineItemsQuery : IRequest<IEnumerable<QuoteLineItemListDto>>
    {
        // No parameters needed for retrieving all quote line items
    }
}
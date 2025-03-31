using MediatR;
using VibeCRM.Shared.DTOs.QuoteLineItem;

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
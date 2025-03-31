using MediatR;
using VibeCRM.Shared.DTOs.QuoteLineItem;

namespace VibeCRM.Application.Features.QuoteLineItem.Queries.GetQuoteLineItemsByProduct
{
    /// <summary>
    /// Query to retrieve all quote line items for a specific product
    /// </summary>
    public class GetQuoteLineItemsByProductQuery : IRequest<IEnumerable<QuoteLineItemDetailsDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product to retrieve line items for
        /// </summary>
        public Guid ProductId { get; set; }
    }
}
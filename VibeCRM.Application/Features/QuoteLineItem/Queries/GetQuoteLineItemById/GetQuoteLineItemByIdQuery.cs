using MediatR;
using VibeCRM.Shared.DTOs.QuoteLineItem;

namespace VibeCRM.Application.Features.QuoteLineItem.Queries.GetQuoteLineItemById
{
    /// <summary>
    /// Query to retrieve a quote line item by its unique identifier
    /// </summary>
    public class GetQuoteLineItemByIdQuery : IRequest<QuoteLineItemDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the quote line item to retrieve
        /// </summary>
        public Guid Id { get; set; }
    }
}
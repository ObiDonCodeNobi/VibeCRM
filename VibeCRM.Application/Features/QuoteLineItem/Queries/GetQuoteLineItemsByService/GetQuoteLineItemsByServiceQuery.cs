using MediatR;
using VibeCRM.Application.Features.QuoteLineItem.DTOs;

namespace VibeCRM.Application.Features.QuoteLineItem.Queries.GetQuoteLineItemsByService
{
    /// <summary>
    /// Query to retrieve all quote line items for a specific service
    /// </summary>
    public class GetQuoteLineItemsByServiceQuery : IRequest<IEnumerable<QuoteLineItemDetailsDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the service to retrieve line items for
        /// </summary>
        public Guid ServiceId { get; set; }
    }
}
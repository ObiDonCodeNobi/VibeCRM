using MediatR;
using VibeCRM.Shared.DTOs.QuoteLineItem;

namespace VibeCRM.Application.Features.QuoteLineItem.Queries.GetQuoteLineItemsByDateRange
{
    /// <summary>
    /// Query to retrieve all quote line items created within a specific date range
    /// </summary>
    public class GetQuoteLineItemsByDateRangeQuery : IRequest<IEnumerable<QuoteLineItemDetailsDto>>
    {
        /// <summary>
        /// Gets or sets the start date of the range
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the range
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
using MediatR;
using VibeCRM.Application.Features.SalesOrder.DTOs;

namespace VibeCRM.Application.Features.SalesOrder.Queries.GetSalesOrderByQuote
{
    /// <summary>
    /// Query for retrieving sales orders associated with a specific quote
    /// </summary>
    public class GetSalesOrderByQuoteQuery : IRequest<IEnumerable<SalesOrderListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the quote
        /// </summary>
        public Guid QuoteId { get; set; }
    }
}
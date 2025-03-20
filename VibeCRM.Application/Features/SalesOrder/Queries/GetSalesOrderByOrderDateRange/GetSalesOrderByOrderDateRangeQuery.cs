using MediatR;
using VibeCRM.Application.Features.SalesOrder.DTOs;

namespace VibeCRM.Application.Features.SalesOrder.Queries.GetSalesOrderByOrderDateRange
{
    /// <summary>
    /// Query for retrieving sales orders within a specific order date range
    /// </summary>
    public class GetSalesOrderByOrderDateRangeQuery : IRequest<IEnumerable<SalesOrderListDto>>
    {
        /// <summary>
        /// Gets or sets the start date of the order date range
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the order date range
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
using MediatR;
using VibeCRM.Application.Features.SalesOrder.DTOs;

namespace VibeCRM.Application.Features.SalesOrder.Queries.GetAllSalesOrders
{
    /// <summary>
    /// Query for retrieving all sales orders
    /// </summary>
    public class GetAllSalesOrdersQuery : IRequest<IEnumerable<SalesOrderListDto>>
    {
        /// <summary>
        /// Gets or sets a value indicating whether to include inactive sales orders
        /// </summary>
        public bool IncludeInactive { get; set; } = false;
    }
}
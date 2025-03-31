using MediatR;
using VibeCRM.Shared.DTOs.SalesOrder;

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
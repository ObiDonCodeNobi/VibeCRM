using MediatR;
using VibeCRM.Application.Features.SalesOrderStatus.DTOs;

namespace VibeCRM.Application.Features.SalesOrderStatus.Queries.GetSalesOrderStatusByOrdinalPosition
{
    /// <summary>
    /// Query for retrieving sales order statuses by their ordinal position.
    /// </summary>
    public class GetSalesOrderStatusByOrdinalPositionQuery : IRequest<IEnumerable<SalesOrderStatusDto>>
    {
        /// <summary>
        /// Gets or sets the ordinal position to retrieve sales order statuses by.
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}
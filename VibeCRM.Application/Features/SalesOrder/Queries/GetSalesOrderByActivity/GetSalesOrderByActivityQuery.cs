using MediatR;
using VibeCRM.Application.Features.SalesOrder.DTOs;

namespace VibeCRM.Application.Features.SalesOrder.Queries.GetSalesOrderByActivity
{
    /// <summary>
    /// Query for retrieving sales orders associated with a specific activity
    /// </summary>
    public class GetSalesOrderByActivityQuery : IRequest<IEnumerable<SalesOrderListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the activity
        /// </summary>
        public Guid ActivityId { get; set; }
    }
}
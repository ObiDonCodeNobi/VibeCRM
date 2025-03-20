using MediatR;
using VibeCRM.Application.Features.SalesOrderStatus.DTOs;

namespace VibeCRM.Application.Features.SalesOrderStatus.Queries.GetSalesOrderStatusByStatus
{
    /// <summary>
    /// Query for retrieving sales order statuses by their status name.
    /// </summary>
    public class GetSalesOrderStatusByStatusQuery : IRequest<IEnumerable<SalesOrderStatusDto>>
    {
        /// <summary>
        /// Gets or sets the status name to retrieve sales order statuses by.
        /// </summary>
        public string Status { get; set; } = string.Empty;
    }
}
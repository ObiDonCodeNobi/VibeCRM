using MediatR;
using VibeCRM.Application.Features.SalesOrderStatus.DTOs;

namespace VibeCRM.Application.Features.SalesOrderStatus.Queries.GetAllSalesOrderStatuses
{
    /// <summary>
    /// Query for retrieving all active sales order statuses.
    /// </summary>
    public class GetAllSalesOrderStatusesQuery : IRequest<IEnumerable<SalesOrderStatusListDto>>
    {
        // No parameters needed for this query
    }
}

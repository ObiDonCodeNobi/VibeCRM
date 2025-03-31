using MediatR;
using VibeCRM.Shared.DTOs.SalesOrder;

namespace VibeCRM.Application.Features.SalesOrder.Queries.GetSalesOrderByNumber
{
    /// <summary>
    /// Query for retrieving a sales order by its number
    /// </summary>
    public class GetSalesOrderByNumberQuery : IRequest<SalesOrderDetailsDto>
    {
        /// <summary>
        /// Gets or sets the number of the sales order to retrieve
        /// </summary>
        public string Number { get; set; } = string.Empty;
    }
}
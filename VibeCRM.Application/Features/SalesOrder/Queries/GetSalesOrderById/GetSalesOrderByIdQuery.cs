using MediatR;
using VibeCRM.Application.Features.SalesOrder.DTOs;

namespace VibeCRM.Application.Features.SalesOrder.Queries.GetSalesOrderById
{
    /// <summary>
    /// Query for retrieving a sales order by its unique identifier
    /// </summary>
    public class GetSalesOrderByIdQuery : IRequest<SalesOrderDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sales order to retrieve
        /// </summary>
        public Guid Id { get; set; }
    }
}
using MediatR;
using VibeCRM.Shared.DTOs.SalesOrderStatus;

namespace VibeCRM.Application.Features.SalesOrderStatus.Queries.GetSalesOrderStatusById
{
    /// <summary>
    /// Query for retrieving a specific sales order status by its unique identifier.
    /// </summary>
    public class GetSalesOrderStatusByIdQuery : IRequest<SalesOrderStatusDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sales order status to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}
using MediatR;
using VibeCRM.Application.Features.SalesOrderLineItem.DTOs;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Queries.GetSalesOrderLineItemById
{
    /// <summary>
    /// Query to retrieve a sales order line item by its unique identifier
    /// </summary>
    public class GetSalesOrderLineItemByIdQuery : IRequest<SalesOrderLineItemDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sales order line item to retrieve
        /// </summary>
        public Guid Id { get; set; }
    }
}
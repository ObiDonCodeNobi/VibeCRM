using MediatR;
using VibeCRM.Shared.DTOs.SalesOrderLineItem;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Queries.GetSalesOrderLineItemsBySalesOrder
{
    /// <summary>
    /// Query to retrieve all sales order line items for a specific sales order
    /// </summary>
    public class GetSalesOrderLineItemsBySalesOrderQuery : IRequest<IEnumerable<SalesOrderLineItemDetailsDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sales order to retrieve line items for
        /// </summary>
        public Guid SalesOrderId { get; set; }
    }
}
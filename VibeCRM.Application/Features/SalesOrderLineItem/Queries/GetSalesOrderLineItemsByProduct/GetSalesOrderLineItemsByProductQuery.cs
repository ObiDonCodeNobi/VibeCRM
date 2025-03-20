using MediatR;
using VibeCRM.Application.Features.SalesOrderLineItem.DTOs;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Queries.GetSalesOrderLineItemsByProduct
{
    /// <summary>
    /// Query to retrieve all sales order line items for a specific product
    /// </summary>
    public class GetSalesOrderLineItemsByProductQuery : IRequest<IEnumerable<SalesOrderLineItemDetailsDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product to retrieve line items for
        /// </summary>
        public Guid ProductId { get; set; }
    }
}
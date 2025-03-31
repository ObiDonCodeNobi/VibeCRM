using MediatR;
using VibeCRM.Shared.DTOs.SalesOrderLineItem;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Queries.GetAllSalesOrderLineItems
{
    /// <summary>
    /// Query to retrieve all active sales order line items
    /// </summary>
    public class GetAllSalesOrderLineItemsQuery : IRequest<IEnumerable<SalesOrderLineItemListDto>>
    {
        /// <summary>
        /// Gets or sets a value indicating whether to include inactive sales order line items in the results
        /// </summary>
        public bool IncludeInactive { get; set; } = false;
    }
}
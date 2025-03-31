using MediatR;
using VibeCRM.Shared.DTOs.SalesOrderLineItem;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Queries.GetSalesOrderLineItemsByService
{
    /// <summary>
    /// Query to retrieve all sales order line items for a specific service
    /// </summary>
    public class GetSalesOrderLineItemsByServiceQuery : IRequest<IEnumerable<SalesOrderLineItemDetailsDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the service to retrieve line items for
        /// </summary>
        public Guid ServiceId { get; set; }
    }
}
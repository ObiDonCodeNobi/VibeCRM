using MediatR;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Queries.GetTotalForSalesOrder
{
    /// <summary>
    /// Query to retrieve the total amount for a specific sales order
    /// </summary>
    public class GetTotalForSalesOrderQuery : IRequest<decimal>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sales order to calculate the total for
        /// </summary>
        public Guid SalesOrderId { get; set; }
    }
}
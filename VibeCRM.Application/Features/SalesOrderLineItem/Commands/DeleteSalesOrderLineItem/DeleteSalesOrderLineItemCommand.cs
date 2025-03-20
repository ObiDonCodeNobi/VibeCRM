using MediatR;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Commands.DeleteSalesOrderLineItem
{
    /// <summary>
    /// Command for deleting a sales order line item
    /// </summary>
    public class DeleteSalesOrderLineItemCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sales order line item to delete
        /// </summary>
        public Guid Id { get; set; }
    }
}
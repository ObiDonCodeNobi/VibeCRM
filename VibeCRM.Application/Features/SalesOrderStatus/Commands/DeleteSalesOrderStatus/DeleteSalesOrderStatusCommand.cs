using MediatR;

namespace VibeCRM.Application.Features.SalesOrderStatus.Commands.DeleteSalesOrderStatus
{
    /// <summary>
    /// Command for soft-deleting an existing sales order status.
    /// </summary>
    public class DeleteSalesOrderStatusCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the sales order status to delete.
        /// </summary>
        public Guid Id { get; set; }
    }
}
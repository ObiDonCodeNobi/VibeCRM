using MediatR;

namespace VibeCRM.Application.Features.SalesOrder.Commands.DeleteSalesOrder
{
    /// <summary>
    /// Command for deleting a sales order
    /// </summary>
    public class DeleteSalesOrderCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sales order to delete
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the user identifier who is deleting this sales order
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}
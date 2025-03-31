using MediatR;
using VibeCRM.Shared.DTOs.SalesOrderStatus;

namespace VibeCRM.Application.Features.SalesOrderStatus.Commands.UpdateSalesOrderStatus
{
    /// <summary>
    /// Command for updating an existing sales order status.
    /// </summary>
    public class UpdateSalesOrderStatusCommand : IRequest<SalesOrderStatusDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the sales order status to update.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the status name (e.g., New, Processing, Completed).
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the sales order status.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for display ordering.
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}
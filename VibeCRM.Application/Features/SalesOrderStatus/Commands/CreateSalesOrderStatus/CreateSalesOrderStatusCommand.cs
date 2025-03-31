using MediatR;
using VibeCRM.Shared.DTOs.SalesOrderStatus;

namespace VibeCRM.Application.Features.SalesOrderStatus.Commands.CreateSalesOrderStatus
{
    /// <summary>
    /// Command for creating a new sales order status.
    /// </summary>
    public class CreateSalesOrderStatusCommand : IRequest<SalesOrderStatusDto>
    {
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
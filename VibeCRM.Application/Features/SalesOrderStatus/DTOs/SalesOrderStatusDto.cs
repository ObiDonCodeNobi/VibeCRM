namespace VibeCRM.Application.Features.SalesOrderStatus.DTOs
{
    /// <summary>
    /// Data Transfer Object for basic sales order status information.
    /// </summary>
    public class SalesOrderStatusDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the sales order status.
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
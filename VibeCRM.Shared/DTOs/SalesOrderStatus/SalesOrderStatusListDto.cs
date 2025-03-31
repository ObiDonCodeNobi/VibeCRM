namespace VibeCRM.Shared.DTOs.SalesOrderStatus
{
    /// <summary>
    /// Data Transfer Object for listing sales order statuses with additional information.
    /// </summary>
    public class SalesOrderStatusListDto
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

        /// <summary>
        /// Gets or sets the count of sales orders with this status.
        /// </summary>
        public int SalesOrderCount { get; set; }
    }
}
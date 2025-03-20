namespace VibeCRM.Application.Features.SalesOrderStatus.DTOs
{
    /// <summary>
    /// Data Transfer Object for detailed sales order status information including audit fields.
    /// </summary>
    public class SalesOrderStatusDetailsDto
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

        /// <summary>
        /// Gets or sets the date and time when the sales order status was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the user who created the sales order status.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the sales order status was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the user who last modified the sales order status.
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the sales order status is active.
        /// </summary>
        public bool Active { get; set; }
    }
}
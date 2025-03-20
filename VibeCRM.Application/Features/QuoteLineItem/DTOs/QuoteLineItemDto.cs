namespace VibeCRM.Application.Features.QuoteLineItem.DTOs
{
    /// <summary>
    /// Base Data Transfer Object for QuoteLineItem information
    /// </summary>
    public class QuoteLineItemDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the quote line item
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the quote identifier that this line item belongs to
        /// </summary>
        public Guid QuoteId { get; set; }

        /// <summary>
        /// Gets or sets the product identifier that this line item represents, if applicable
        /// </summary>
        public Guid? ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product name, if applicable
        /// </summary>
        public string? ProductName { get; set; }

        /// <summary>
        /// Gets or sets the service identifier that this line item represents, if applicable
        /// </summary>
        public Guid? ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the service name, if applicable
        /// </summary>
        public string? ServiceName { get; set; }

        /// <summary>
        /// Gets or sets the description of the line item
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the product or service
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product or service
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the discount percentage applied to this line item, if any
        /// </summary>
        public decimal? DiscountPercentage { get; set; }

        /// <summary>
        /// Gets or sets the fixed discount amount applied to this line item, if any
        /// </summary>
        public decimal? DiscountAmount { get; set; }

        /// <summary>
        /// Gets or sets the tax percentage applied to this line item
        /// </summary>
        public decimal? TaxPercentage { get; set; }

        /// <summary>
        /// Gets or sets the line number for sorting and display purposes
        /// </summary>
        public int LineNumber { get; set; }

        /// <summary>
        /// Gets or sets additional notes or comments about the quote line item
        /// </summary>
        public string Notes { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets whether the quote line item is active
        /// </summary>
        public bool Active { get; set; }
    }
}
namespace VibeCRM.Shared.DTOs.InvoiceLineItem
{
    /// <summary>
    /// Data transfer object for invoice line item information
    /// </summary>
    public class InvoiceLineItemDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the invoice line item
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the invoice identifier that this line item belongs to
        /// </summary>
        public Guid InvoiceId { get; set; }

        /// <summary>
        /// Gets or sets the product identifier that this line item represents, if applicable
        /// </summary>
        public Guid? ProductId { get; set; }

        /// <summary>
        /// Gets or sets the service identifier that this line item represents, if applicable
        /// </summary>
        public Guid? ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the sales order line item identifier that this invoice line item is derived from, if applicable
        /// </summary>
        public Guid? SalesOrderLineItemId { get; set; }

        /// <summary>
        /// Gets or sets the description of the line item
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the product or service
        /// </summary>
        public decimal Quantity { get; set; } = 1;

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
        /// Gets or sets additional notes or comments about the invoice line item
        /// </summary>
        public string? Notes { get; set; }
    }
}
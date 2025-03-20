namespace VibeCRM.Application.Features.Invoice.DTOs
{
    /// <summary>
    /// Data Transfer Object for invoice line items.
    /// Used for transferring invoice line item data between layers.
    /// </summary>
    public class InvoiceLineItemDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the invoice line item.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the invoice ID that this line item belongs to.
        /// </summary>
        public Guid InvoiceId { get; set; }

        /// <summary>
        /// Gets or sets the service ID associated with this line item.
        /// </summary>
        public Guid ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the name of the service for this line item.
        /// </summary>
        public string ServiceName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the line item.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the service.
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the service.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the discount amount applied to this line item.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the tax amount applied to this line item.
        /// </summary>
        public decimal Tax { get; set; }

        /// <summary>
        /// Gets or sets the total amount for this line item (Quantity * UnitPrice - Discount + Tax).
        /// </summary>
        public decimal TotalAmount { get; set; }
    }
}
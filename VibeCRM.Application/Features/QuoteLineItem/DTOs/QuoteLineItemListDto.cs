namespace VibeCRM.Application.Features.QuoteLineItem.DTOs
{
    /// <summary>
    /// Data Transfer Object for listing QuoteLineItem information
    /// </summary>
    public class QuoteLineItemListDto
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
        /// Gets or sets the line number for sorting and display purposes
        /// </summary>
        public int LineNumber { get; set; }

        /// <summary>
        /// Gets the extended price of the line item (unit price × quantity)
        /// </summary>
        public decimal ExtendedPrice => UnitPrice * Quantity;

        /// <summary>
        /// Gets or sets the date and time when the quote line item was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the quote line item was last modified
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}
namespace VibeCRM.Application.Features.Quote.DTOs
{
    /// <summary>
    /// Data Transfer Object for QuoteLineItem information
    /// </summary>
    public class QuoteLineItemDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the quote line item
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
        public string? Notes { get; set; }

        /// <summary>
        /// Gets the extended price of the line item (unit price × quantity)
        /// </summary>
        public decimal ExtendedPrice => UnitPrice * Quantity;

        /// <summary>
        /// Gets the discount amount for this line item
        /// </summary>
        public decimal DiscountTotal => CalculateDiscountAmount();

        /// <summary>
        /// Gets the tax amount for this line item
        /// </summary>
        public decimal TaxTotal => CalculateTaxAmount();

        /// <summary>
        /// Gets the total price for this line item, including discounts and taxes
        /// </summary>
        public decimal TotalPrice => ExtendedPrice - DiscountTotal + TaxTotal;

        /// <summary>
        /// Calculates the discount amount based on either the discount percentage or fixed discount amount
        /// </summary>
        /// <returns>The calculated discount amount</returns>
        private decimal CalculateDiscountAmount()
        {
            if (DiscountAmount.HasValue && DiscountAmount.Value > 0)
            {
                return Math.Min(DiscountAmount.Value, ExtendedPrice);
            }

            if (DiscountPercentage.HasValue && DiscountPercentage.Value > 0)
            {
                return ExtendedPrice * (DiscountPercentage.Value / 100m);
            }

            return 0;
        }

        /// <summary>
        /// Calculates the tax amount based on the taxable price and tax percentage
        /// </summary>
        /// <returns>The calculated tax amount</returns>
        private decimal CalculateTaxAmount()
        {
            if (!TaxPercentage.HasValue || TaxPercentage.Value <= 0)
            {
                return 0;
            }

            var taxableAmount = ExtendedPrice - DiscountTotal;
            return taxableAmount * (TaxPercentage.Value / 100m);
        }
    }
}

namespace VibeCRM.Application.Features.QuoteLineItem.DTOs
{
    /// <summary>
    /// Detailed Data Transfer Object for QuoteLineItem information including audit properties
    /// </summary>
    public class QuoteLineItemDetailsDto : QuoteLineItemDto
    {
        /// <summary>
        /// Gets or sets the identifier of the user who created the quote line item
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the quote line item was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the quote line item
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the quote line item was last modified
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets the extended price of the line item (unit price × quantity)
        /// </summary>
        public decimal ExtendedPrice => UnitPrice * Quantity;

        /// <summary>
        /// Gets the discount amount for this line item
        /// </summary>
        public decimal DiscountValue => CalculateDiscountAmount();

        /// <summary>
        /// Gets the tax amount for this line item
        /// </summary>
        public decimal TaxAmount => CalculateTaxAmount();

        /// <summary>
        /// Gets the total price for this line item, including discounts and taxes
        /// </summary>
        public decimal TotalPrice => CalculateTotalPrice();

        /// <summary>
        /// Calculates the discount amount based on either the discount percentage or fixed discount amount
        /// </summary>
        /// <returns>The calculated discount amount</returns>
        private decimal CalculateDiscountAmount()
        {
            var extendedPrice = ExtendedPrice;

            if (DiscountAmount.HasValue && DiscountAmount.Value > 0)
            {
                return Math.Min(DiscountAmount.Value, extendedPrice);
            }

            if (DiscountPercentage.HasValue && DiscountPercentage.Value > 0)
            {
                return extendedPrice * (DiscountPercentage.Value / 100m);
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

            var taxableAmount = ExtendedPrice - DiscountValue;
            return taxableAmount * (TaxPercentage.Value / 100m);
        }

        /// <summary>
        /// Calculates the total price including discounts and taxes
        /// </summary>
        /// <returns>The calculated total price</returns>
        private decimal CalculateTotalPrice()
        {
            return ExtendedPrice - DiscountValue + TaxAmount;
        }
    }
}
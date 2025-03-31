namespace VibeCRM.Shared.DTOs.SalesOrderLineItem
{
    /// <summary>
    /// Detailed Data Transfer Object for SalesOrderLineItem information including audit properties
    /// </summary>
    public class SalesOrderLineItemDetailsDto : SalesOrderLineItemDto
    {
        /// <summary>
        /// Gets or sets the identifier of the user who created the sales order line item
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the sales order line item was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the sales order line item
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the sales order line item was last modified
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
            if (DiscountAmount.HasValue)
            {
                return DiscountAmount.Value;
            }

            if (DiscountPercentage.HasValue)
            {
                return ExtendedPrice * (DiscountPercentage.Value / 100);
            }

            return 0;
        }

        /// <summary>
        /// Calculates the tax amount based on whether the item is taxable and the tax rate
        /// </summary>
        /// <returns>The calculated tax amount</returns>
        private decimal CalculateTaxAmount()
        {
            if (!IsTaxable || !TaxRate.HasValue)
            {
                return 0;
            }

            decimal priceAfterDiscount = ExtendedPrice - DiscountValue;
            return priceAfterDiscount * (TaxRate.Value / 100);
        }

        /// <summary>
        /// Calculates the total price including discounts and taxes
        /// </summary>
        /// <returns>The calculated total price</returns>
        private decimal CalculateTotalPrice()
        {
            decimal priceAfterDiscount = ExtendedPrice - DiscountValue;
            return priceAfterDiscount + TaxAmount;
        }
    }
}
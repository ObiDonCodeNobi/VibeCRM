using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;

namespace VibeCRM.Domain.Entities.BusinessEntities
{
    /// <summary>
    /// Represents a line item within a quote in the system.
    /// </summary>
    /// <remarks>
    /// Quote line items detail the specific products, services, or charges that make up a quote.
    /// Each line item typically references a product or service and specifies quantity, price, and other details.
    /// </remarks>
    public class QuoteLineItem : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuoteLineItem"/> class.
        /// </summary>
        public QuoteLineItem()
        {
            Id = Guid.NewGuid();
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the unique identifier for the quote line item.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid QuoteLineItemId
        {
            get => Id;
            set => Id = value;
        }

        /// <summary>
        /// Gets or sets the quote identifier that this line item belongs to.
        /// </summary>
        public Guid QuoteId { get; set; }

        /// <summary>
        /// Gets or sets the quote that this line item belongs to.
        /// </summary>
        public Quote? Quote { get; set; }

        /// <summary>
        /// Gets or sets the product identifier that this line item represents, if applicable.
        /// </summary>
        public Guid? ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product that this line item represents, if applicable.
        /// </summary>
        public Product? Product { get; set; }

        /// <summary>
        /// Gets or sets the service identifier that this line item represents, if applicable.
        /// </summary>
        public Guid? ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the description of the line item.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product or service.
        /// </summary>
        public decimal Quantity { get; set; } = 1;

        /// <summary>
        /// Gets or sets the unit price of the product or service.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the discount percentage applied to this line item, if any.
        /// </summary>
        public decimal? DiscountPercentage { get; set; }

        /// <summary>
        /// Gets or sets the fixed discount amount applied to this line item, if any.
        /// </summary>
        public decimal? DiscountAmount { get; set; }

        /// <summary>
        /// Gets or sets the tax percentage applied to this line item.
        /// </summary>
        public decimal? TaxPercentage { get; set; }

        /// <summary>
        /// Gets or sets the line number for sorting and display purposes.
        /// </summary>
        public int LineNumber { get; set; }

        /// <summary>
        /// Gets or sets additional notes or comments about the quote line item.
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Gets the extended price of the line item (unit price × quantity).
        /// </summary>
        /// <returns>The calculated extended price before discounts and taxes.</returns>
        public decimal GetExtendedPrice()
        {
            return UnitPrice * Quantity;
        }

        /// <summary>
        /// Gets the discount amount for this line item.
        /// </summary>
        /// <returns>
        /// The calculated discount amount based on either the discount percentage or fixed discount amount.
        /// </returns>
        public decimal GetDiscountAmount()
        {
            var extendedPrice = GetExtendedPrice();

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
        /// Gets the tax amount for this line item.
        /// </summary>
        /// <returns>The calculated tax amount based on the taxable price and tax percentage.</returns>
        public decimal GetTaxAmount()
        {
            if (!TaxPercentage.HasValue || TaxPercentage.Value <= 0)
            {
                return 0;
            }

            var taxableAmount = GetExtendedPrice() - GetDiscountAmount();
            return taxableAmount * (TaxPercentage.Value / 100m);
        }

        /// <summary>
        /// Gets the total price for this line item, including discounts and taxes.
        /// </summary>
        /// <returns>The calculated total price for this line item.</returns>
        public decimal GetTotalPrice()
        {
            return GetExtendedPrice() - GetDiscountAmount() + GetTaxAmount();
        }
    }
}
using MediatR;
using VibeCRM.Shared.DTOs.QuoteLineItem;

namespace VibeCRM.Application.Features.QuoteLineItem.Commands.UpdateQuoteLineItem
{
    /// <summary>
    /// Command for updating an existing quote line item
    /// </summary>
    public class UpdateQuoteLineItemCommand : IRequest<QuoteLineItemDetailsDto>
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
        /// Gets or sets the service identifier that this line item represents, if applicable
        /// </summary>
        public Guid? ServiceId { get; set; }

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
        /// Gets or sets the identifier of the user updating the quote line item
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}
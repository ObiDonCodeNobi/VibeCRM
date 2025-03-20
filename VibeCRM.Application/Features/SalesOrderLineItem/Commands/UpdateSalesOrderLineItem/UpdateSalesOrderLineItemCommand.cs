using MediatR;
using VibeCRM.Application.Features.SalesOrderLineItem.DTOs;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Commands.UpdateSalesOrderLineItem
{
    /// <summary>
    /// Command for updating an existing sales order line item
    /// </summary>
    public class UpdateSalesOrderLineItemCommand : IRequest<SalesOrderLineItemDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the sales order line item
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the sales order identifier that this line item belongs to
        /// </summary>
        public Guid SalesOrderId { get; set; }

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
        /// Gets or sets whether the line item is taxable
        /// </summary>
        public bool IsTaxable { get; set; }

        /// <summary>
        /// Gets or sets the tax rate applied to this line item, if taxable
        /// </summary>
        public decimal? TaxRate { get; set; }

        /// <summary>
        /// Gets or sets the notes or additional information for this line item
        /// </summary>
        public string Notes { get; set; } = string.Empty;
    }
}
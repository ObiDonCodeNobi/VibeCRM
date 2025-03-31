namespace VibeCRM.Shared.DTOs.SalesOrderLineItem
{
    /// <summary>
    /// Data Transfer Object for SalesOrderLineItem list view information
    /// </summary>
    public class SalesOrderLineItemListDto
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
        /// Gets the extended price of the line item (unit price × quantity)
        /// </summary>
        public decimal ExtendedPrice { get; set; }
    }
}
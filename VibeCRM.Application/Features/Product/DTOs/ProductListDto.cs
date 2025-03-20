namespace VibeCRM.Application.Features.Product.DTOs
{
    /// <summary>
    /// Data Transfer Object for listing products in UI components
    /// </summary>
    public class ProductListDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the product type identifier
        /// </summary>
        public Guid ProductTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the product type
        /// </summary>
        public string? ProductTypeName { get; set; }

        /// <summary>
        /// Gets or sets the name of the product
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the product
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the count of quote line items associated with this product
        /// </summary>
        public int QuoteLineItemCount { get; set; }

        /// <summary>
        /// Gets or sets the count of sales order line items associated with this product
        /// </summary>
        public int SalesOrderLineItemCount { get; set; }

        /// <summary>
        /// Gets or sets the count of product groups this product belongs to
        /// </summary>
        public int ProductGroupCount { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the product was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the product was last modified
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}
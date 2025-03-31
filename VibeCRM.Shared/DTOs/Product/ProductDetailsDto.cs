using VibeCRM.Shared.DTOs.ProductGroup;
using VibeCRM.Shared.DTOs.ProductType;
using VibeCRM.Shared.DTOs.QuoteLineItem;
using VibeCRM.Shared.DTOs.SalesOrderLineItem;

namespace VibeCRM.Shared.DTOs.Product
{
    /// <summary>
    /// Data Transfer Object for detailed product information including related entities
    /// </summary>
    public class ProductDetailsDto
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
        /// Gets or sets the name of the product
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the product
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the product type details
        /// </summary>
        public ProductTypeDetailsDto? ProductType { get; set; }

        /// <summary>
        /// Gets or sets the collection of quote line items that reference this product
        /// </summary>
        public ICollection<QuoteLineItemDto> QuoteLineItems { get; set; } = new List<QuoteLineItemDto>();

        /// <summary>
        /// Gets or sets the collection of sales order line items that reference this product
        /// </summary>
        public ICollection<SalesOrderLineItemDto> SalesOrderLineItems { get; set; } = new List<SalesOrderLineItemDto>();

        /// <summary>
        /// Gets or sets the collection of product groups this product belongs to
        /// </summary>
        public ICollection<ProductGroupListDto> ProductGroups { get; set; } = new List<ProductGroupListDto>();

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
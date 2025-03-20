using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.TypeStatusEntities;
using System.Collections.Generic;
using VibeCRM.Domain.Common.Base;

namespace VibeCRM.Domain.Entities.BusinessEntities
{
    /// <summary>
    /// Represents a product in the system.
    /// </summary>
    public class Product : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        public Product()
        {
            Id = Guid.NewGuid();
            Name = string.Empty;
            Description = string.Empty;
            QuoteLineItems = new HashSet<QuoteLineItem>();
            SalesOrderLineItems = new HashSet<SalesOrderLineItem>();
            ProductGroups = new HashSet<ProductGroup>();
        }

        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid ProductId
        {
            get => Id;
            set => Id = value;
        }

        /// <summary>
        /// Gets or sets the product type identifier associated with this product.
        /// </summary>
        public Guid ProductTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the product type associated with this product.
        /// </summary>
        public ProductType? ProductType { get; set; }

        /// <summary>
        /// Gets or sets the collection of quote line items that reference this product.
        /// </summary>
        public ICollection<QuoteLineItem> QuoteLineItems { get; set; }

        /// <summary>
        /// Gets or sets the collection of sales order line items that reference this product.
        /// </summary>
        public ICollection<SalesOrderLineItem> SalesOrderLineItems { get; set; }

        /// <summary>
        /// Gets or sets the collection of product groups this product belongs to.
        /// </summary>
        public ICollection<ProductGroup> ProductGroups { get; set; }
    }
}
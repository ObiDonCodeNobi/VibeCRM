using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;
using VibeCRM.Domain.Entities.BusinessEntities;

namespace VibeCRM.Domain.Entities.TypeStatusEntities
{
    /// <summary>
    /// Represents a product type in the system, such as Hardware, Software, Service, etc.
    /// Used to categorize products for organization and reporting.
    /// </summary>
    public class ProductType : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductType"/> class.
        /// </summary>
        public ProductType()
        {
            Products = new List<Product>();
            Id = Guid.NewGuid();
            Type = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the product type identifier that directly maps to the ProductTypeId database column.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid ProductTypeId { get => Id; set => Id = value; }

        /// <summary>
        /// Gets or sets the type name (e.g., "Hardware", "Software", "Service").
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the description of the product type with details about products in this category.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ordinal position for sorting product types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        // The Active property is inherited from BaseAuditableEntity<Guid>

        /// <summary>
        /// Gets or sets the collection of products of this type.
        /// </summary>
        public ICollection<Product> Products { get; set; }
    }
}
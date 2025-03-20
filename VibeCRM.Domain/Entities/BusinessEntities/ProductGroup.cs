using VibeCRM.Domain.Common.Base;
using VibeCRM.Domain.Common.Interfaces;

namespace VibeCRM.Domain.Entities.BusinessEntities
{
    /// <summary>
    /// Represents a group or category of products in the system.
    /// </summary>
    /// <remarks>
    /// Product groups allow for organizing products into logical categories for easier navigation,
    /// reporting, and management. A product can belong to multiple groups.
    /// </remarks>
    public class ProductGroup : BaseAuditableEntity<Guid>, ISoftDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductGroup"/> class.
        /// </summary>
        public ProductGroup()
        { Id = Guid.NewGuid(); Name = string.Empty; Description = string.Empty; Products = new List<Product>(); }

        /// <summary>
        /// Gets or sets the unique identifier for the product group.
        /// </summary>
        /// <remarks>This property maps to the Id property from the base class.</remarks>
        public Guid ProductGroupId
        {
            get => Id;
            set => Id = value;
        }

        /// <summary>
        /// Gets or sets the name of the product group.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the product group.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the parent product group identifier, if this is a subgroup.
        /// </summary>
        public Guid? ParentProductGroupId { get; set; }

        /// <summary>
        /// Gets or sets the display order for sorting product groups in listings.
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the collection of products that belong to this group.
        /// </summary>
        public ICollection<Product> Products { get; set; }
    }
}
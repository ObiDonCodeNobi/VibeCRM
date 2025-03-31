namespace VibeCRM.Shared.DTOs.ProductGroup
{
    /// <summary>
    /// Base Data Transfer Object for ProductGroup entities.
    /// Contains the essential properties needed for basic operations.
    /// </summary>
    public class ProductGroupDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product group.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product group.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the product group.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the parent product group identifier, if this is a subgroup.
        /// </summary>
        public Guid? ParentProductGroupId { get; set; }

        /// <summary>
        /// Gets or sets the display order for sorting product groups in listings.
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets whether this product group is active (not soft-deleted).
        /// When true, the product group is active and visible in queries.
        /// When false, the product group is considered deleted but remains in the database.
        /// </summary>
        public bool Active { get; set; } = true;
    }
}
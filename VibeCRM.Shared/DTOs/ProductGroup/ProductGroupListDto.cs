namespace VibeCRM.Shared.DTOs.ProductGroup
{
    /// <summary>
    /// List Data Transfer Object for ProductGroup entities.
    /// Contains essential information for displaying product groups in lists and dropdowns.
    /// </summary>
    public class ProductGroupListDto
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
        /// Gets or sets the name of the parent product group, if this is a subgroup.
        /// </summary>
        public string? ParentProductGroupName { get; set; }

        /// <summary>
        /// Gets or sets the display order for sorting product groups in listings.
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the count of products in this group.
        /// </summary>
        public int ProductCount { get; set; }

        /// <summary>
        /// Gets or sets the count of child product groups under this group.
        /// </summary>
        public int ChildGroupCount { get; set; }
    }
}
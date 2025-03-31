namespace VibeCRM.Shared.DTOs.ProductGroup
{
    /// <summary>
    /// Detailed Data Transfer Object for ProductGroup entities.
    /// Contains comprehensive information about a product group, including related data.
    /// </summary>
    public class ProductGroupDetailsDto : ProductGroupDto
    {
        /// <summary>
        /// Gets or sets the name of the parent product group, if this is a subgroup.
        /// </summary>
        public string? ParentProductGroupName { get; set; }

        /// <summary>
        /// Gets or sets the count of products in this group.
        /// </summary>
        public int ProductCount { get; set; }

        /// <summary>
        /// Gets or sets the count of child product groups under this group.
        /// </summary>
        public int ChildGroupCount { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the product group.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the last modification date of the product group.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created the product group.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the product group.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the name of the user who created the product group.
        /// </summary>
        public string CreatedByName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the user who last modified the product group.
        /// </summary>
        public string ModifiedByName { get; set; } = string.Empty;
    }
}
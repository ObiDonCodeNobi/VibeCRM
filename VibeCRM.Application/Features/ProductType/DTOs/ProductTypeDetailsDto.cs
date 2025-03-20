namespace VibeCRM.Application.Features.ProductType.DTOs
{
    /// <summary>
    /// Data Transfer Object for detailed product type information.
    /// Includes audit information and additional details for detailed views.
    /// </summary>
    public class ProductTypeDetailsDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product type.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the type name (e.g., "Hardware", "Software", "Service").
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the product type with details about products in this category.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting product types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// Gets or sets the count of products associated with this product type.
        /// </summary>
        public int ProductCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the product type is active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the product type was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created the product type.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the product type was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified the product type.
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}
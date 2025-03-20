namespace VibeCRM.Application.Features.Product.DTOs
{
    /// <summary>
    /// Data Transfer Object for basic product information
    /// </summary>
    public class ProductDto
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
        /// Gets or sets the date and time when the product was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the product was last modified
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}
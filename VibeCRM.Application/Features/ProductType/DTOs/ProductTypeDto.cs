using System;

namespace VibeCRM.Application.Features.ProductType.DTOs
{
    /// <summary>
    /// Data Transfer Object for basic product type information.
    /// Used for standard operations and list displays.
    /// </summary>
    public class ProductTypeDto
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
    }
}

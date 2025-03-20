using MediatR;
using VibeCRM.Application.Features.ProductType.DTOs;

namespace VibeCRM.Application.Features.ProductType.Commands.UpdateProductType
{
    /// <summary>
    /// Command for updating an existing product type.
    /// </summary>
    public class UpdateProductTypeCommand : IRequest<ProductTypeDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product type to update.
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
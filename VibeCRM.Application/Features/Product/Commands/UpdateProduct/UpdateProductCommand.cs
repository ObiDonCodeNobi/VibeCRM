using MediatR;
using VibeCRM.Shared.DTOs.Product;

namespace VibeCRM.Application.Features.Product.Commands.UpdateProduct
{
    /// <summary>
    /// Command to update an existing product
    /// </summary>
    public class UpdateProductCommand : IRequest<ProductDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product to update
        /// </summary>
        public Guid ProductId { get; set; }

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
        /// Gets or sets the user who modified the product
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;
    }
}
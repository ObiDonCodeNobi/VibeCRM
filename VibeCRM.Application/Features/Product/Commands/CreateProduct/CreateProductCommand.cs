using MediatR;
using VibeCRM.Application.Features.Product.DTOs;

namespace VibeCRM.Application.Features.Product.Commands.CreateProduct
{
    /// <summary>
    /// Command to create a new product
    /// </summary>
    public class CreateProductCommand : IRequest<ProductDetailsDto>
    {
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
        /// Gets or sets the user who created the product
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;
    }
}
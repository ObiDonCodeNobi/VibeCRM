using MediatR;
using System;
using VibeCRM.Application.Features.Product.DTOs;

namespace VibeCRM.Application.Features.Product.Queries.GetProductById
{
    /// <summary>
    /// Query to retrieve a product by its unique identifier
    /// </summary>
    public class GetProductByIdQuery : IRequest<ProductDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product to retrieve
        /// </summary>
        public Guid ProductId { get; set; }
    }
}

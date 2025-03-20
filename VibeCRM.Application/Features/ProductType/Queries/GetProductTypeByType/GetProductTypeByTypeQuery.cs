using MediatR;
using VibeCRM.Application.Features.ProductType.DTOs;

namespace VibeCRM.Application.Features.ProductType.Queries.GetProductTypeByType
{
    /// <summary>
    /// Query for retrieving a product type by its type name.
    /// </summary>
    public class GetProductTypeByTypeQuery : IRequest<ProductTypeDto>
    {
        /// <summary>
        /// Gets or sets the type name of the product type to retrieve.
        /// </summary>
        public string Type { get; set; } = string.Empty;
    }
}
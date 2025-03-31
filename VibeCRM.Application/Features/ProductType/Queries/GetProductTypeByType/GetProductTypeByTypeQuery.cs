using MediatR;
using VibeCRM.Shared.DTOs.ProductType;

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
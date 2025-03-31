using MediatR;
using VibeCRM.Shared.DTOs.ProductType;

namespace VibeCRM.Application.Features.ProductType.Queries.GetProductTypeByOrdinalPosition
{
    /// <summary>
    /// Query for retrieving a product type by its ordinal position.
    /// </summary>
    public class GetProductTypeByOrdinalPositionQuery : IRequest<ProductTypeDto>
    {
        /// <summary>
        /// Gets or sets the ordinal position of the product type to retrieve.
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}
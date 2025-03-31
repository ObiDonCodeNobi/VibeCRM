using MediatR;
using VibeCRM.Shared.DTOs.Product;

namespace VibeCRM.Application.Features.Product.Queries.GetProductsByProductType
{
    /// <summary>
    /// Query to retrieve all products of a specific product type
    /// </summary>
    public class GetProductsByProductTypeQuery : IRequest<IEnumerable<ProductListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product type to filter products by
        /// </summary>
        public Guid ProductTypeId { get; set; }
    }
}
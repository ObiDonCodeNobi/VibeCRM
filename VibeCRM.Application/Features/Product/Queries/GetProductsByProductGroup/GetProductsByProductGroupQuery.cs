using MediatR;
using VibeCRM.Application.Features.Product.DTOs;

namespace VibeCRM.Application.Features.Product.Queries.GetProductsByProductGroup
{
    /// <summary>
    /// Query to retrieve all products that belong to a specific product group
    /// </summary>
    public class GetProductsByProductGroupQuery : IRequest<IEnumerable<ProductListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product group to filter products by
        /// </summary>
        public Guid ProductGroupId { get; set; }
    }
}
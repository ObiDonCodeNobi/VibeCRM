using MediatR;
using VibeCRM.Application.Features.Product.DTOs;

namespace VibeCRM.Application.Features.Product.Queries.GetAllProducts
{
    /// <summary>
    /// Query to retrieve all active products
    /// </summary>
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductListDto>>
    {
        // No parameters needed for this query as it retrieves all active products
    }
}
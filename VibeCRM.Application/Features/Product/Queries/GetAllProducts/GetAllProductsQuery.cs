using MediatR;
using VibeCRM.Shared.DTOs.Product;

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
using MediatR;
using VibeCRM.Application.Features.ProductType.DTOs;

namespace VibeCRM.Application.Features.ProductType.Queries.GetAllProductTypes
{
    /// <summary>
    /// Query for retrieving all product types.
    /// </summary>
    public class GetAllProductTypesQuery : IRequest<IEnumerable<ProductTypeListDto>>
    {
        // No parameters needed for this query
    }
}
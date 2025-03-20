using MediatR;
using VibeCRM.Application.Features.ProductType.DTOs;

namespace VibeCRM.Application.Features.ProductType.Queries.GetDefaultProductType
{
    /// <summary>
    /// Query for retrieving the default product type.
    /// </summary>
    public class GetDefaultProductTypeQuery : IRequest<ProductTypeDto>
    {
        // No parameters needed for this query
    }
}
using MediatR;
using VibeCRM.Shared.DTOs.ProductGroup;

namespace VibeCRM.Application.Features.ProductGroup.Queries.GetAllProductGroups
{
    /// <summary>
    /// Query to retrieve all active product groups in the system.
    /// This is used in the CQRS pattern as the request object for retrieving all product groups.
    /// </summary>
    public class GetAllProductGroupsQuery : IRequest<IEnumerable<ProductGroupListDto>>
    {
        // No parameters needed for this query as it retrieves all product groups
    }
}
using MediatR;
using VibeCRM.Application.Features.ProductGroup.DTOs;

namespace VibeCRM.Application.Features.ProductGroup.Queries.GetRootProductGroups
{
    /// <summary>
    /// Query to retrieve all root-level product groups (those without a parent).
    /// This is used in the CQRS pattern as the request object for retrieving top-level product groups.
    /// </summary>
    public class GetRootProductGroupsQuery : IRequest<IEnumerable<ProductGroupListDto>>
    {
        // No parameters needed for this query as it retrieves all root-level product groups
    }
}
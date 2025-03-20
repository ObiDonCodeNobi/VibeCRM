using MediatR;
using VibeCRM.Application.Features.Role.DTOs;

namespace VibeCRM.Application.Features.Role.Queries.GetAllRoles
{
    /// <summary>
    /// Query to retrieve all active roles
    /// </summary>
    public class GetAllRolesQuery : IRequest<IEnumerable<RoleListDto>>
    {
        // This query doesn't require any parameters as it retrieves all active roles
    }
}
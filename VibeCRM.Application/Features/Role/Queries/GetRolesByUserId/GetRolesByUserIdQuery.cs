using MediatR;
using VibeCRM.Application.Features.Role.DTOs;

namespace VibeCRM.Application.Features.Role.Queries.GetRolesByUserId
{
    /// <summary>
    /// Query to retrieve all roles assigned to a specific user
    /// </summary>
    public class GetRolesByUserIdQuery : IRequest<IEnumerable<RoleListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user
        /// </summary>
        public Guid UserId { get; set; }
    }
}
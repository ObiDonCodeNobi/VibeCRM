using MediatR;
using VibeCRM.Application.Features.User.DTOs;

namespace VibeCRM.Application.Features.User.Queries.GetUsersByRoleId
{
    /// <summary>
    /// Query to retrieve users by role ID
    /// </summary>
    public class GetUsersByRoleIdQuery : IRequest<IEnumerable<UserListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the role to retrieve users for
        /// </summary>
        public Guid RoleId { get; set; }
    }
}
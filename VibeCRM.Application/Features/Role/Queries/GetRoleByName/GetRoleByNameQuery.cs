using MediatR;
using VibeCRM.Application.Features.Role.DTOs;

namespace VibeCRM.Application.Features.Role.Queries.GetRoleByName
{
    /// <summary>
    /// Query to retrieve a role by its name
    /// </summary>
    public class GetRoleByNameQuery : IRequest<RoleDetailsDto>
    {
        /// <summary>
        /// Gets or sets the name of the role to retrieve
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
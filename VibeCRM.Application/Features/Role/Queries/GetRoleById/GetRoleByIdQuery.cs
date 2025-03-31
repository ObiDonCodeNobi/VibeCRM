using MediatR;
using VibeCRM.Shared.DTOs.Role;

namespace VibeCRM.Application.Features.Role.Queries.GetRoleById
{
    /// <summary>
    /// Query to retrieve a role by its unique identifier
    /// </summary>
    public class GetRoleByIdQuery : IRequest<RoleDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the role to retrieve
        /// </summary>
        public Guid Id { get; set; }
    }
}
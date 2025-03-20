using MediatR;
using VibeCRM.Application.Features.Role.DTOs;

namespace VibeCRM.Application.Features.Role.Commands.CreateRole
{
    /// <summary>
    /// Command for creating a new role
    /// </summary>
    public class CreateRoleCommand : IRequest<RoleDetailsDto>
    {
        /// <summary>
        /// Gets or sets the role name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the role description
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the identifier of the user creating the role
        /// </summary>
        public Guid CreatedBy { get; set; }
    }
}
using MediatR;
using VibeCRM.Shared.DTOs.Role;

namespace VibeCRM.Application.Features.Role.Commands.UpdateRole
{
    /// <summary>
    /// Command for updating an existing role
    /// </summary>
    public class UpdateRoleCommand : IRequest<RoleDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the role to update
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the role name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the role description
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the identifier of the user modifying the role
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}
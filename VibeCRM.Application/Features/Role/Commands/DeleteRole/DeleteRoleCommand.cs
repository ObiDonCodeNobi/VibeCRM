using MediatR;

namespace VibeCRM.Application.Features.Role.Commands.DeleteRole
{
    /// <summary>
    /// Command for deleting a role
    /// </summary>
    public class DeleteRoleCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the role to delete
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user performing the deletion
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}
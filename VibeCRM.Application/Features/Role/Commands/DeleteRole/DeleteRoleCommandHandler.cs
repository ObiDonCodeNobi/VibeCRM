using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Role.Commands.DeleteRole
{
    /// <summary>
    /// Handler for processing the DeleteRoleCommand
    /// </summary>
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<DeleteRoleCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRoleCommandHandler"/> class
        /// </summary>
        /// <param name="roleRepository">The role repository</param>
        /// <param name="logger">The logger</param>
        public DeleteRoleCommandHandler(
            IRoleRepository roleRepository,
            ILogger<DeleteRoleCommandHandler> logger)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteRoleCommand
        /// </summary>
        /// <param name="request">The command to delete a role</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>True if the role was successfully deleted, otherwise false</returns>
        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting role with ID {RoleId}", request.Id);

            try
            {
                // Get the existing role
                var existingRole = await _roleRepository.GetByIdAsync(request.Id, cancellationToken);

                if (existingRole == null || !existingRole.Active)
                {
                    _logger.LogWarning("Role with ID {RoleId} not found or already inactive", request.Id);
                    return false;
                }

                // Update audit properties before soft delete
                existingRole.ModifiedBy = request.ModifiedBy;
                existingRole.ModifiedDate = DateTime.UtcNow;

                // Soft delete the role
                await _roleRepository.DeleteAsync(request.Id, cancellationToken);

                _logger.LogInformation("Successfully deleted role with ID {RoleId}", request.Id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting role with ID {RoleId}", request.Id);
                throw;
            }
        }
    }
}
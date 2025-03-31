using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Role;

namespace VibeCRM.Application.Features.Role.Commands.UpdateRole
{
    /// <summary>
    /// Handler for processing the UpdateRoleCommand
    /// </summary>
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, RoleDetailsDto>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateRoleCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRoleCommandHandler"/> class
        /// </summary>
        /// <param name="roleRepository">The role repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public UpdateRoleCommandHandler(
            IRoleRepository roleRepository,
            IMapper mapper,
            ILogger<UpdateRoleCommandHandler> logger)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateRoleCommand
        /// </summary>
        /// <param name="request">The command to update a role</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The updated role details</returns>
        public async Task<RoleDetailsDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating role with ID {RoleId}", request.Id);

            try
            {
                // Get the existing role
                var existingRole = await _roleRepository.GetByIdAsync(request.Id, cancellationToken);

                if (existingRole == null || !existingRole.Active)
                {
                    _logger.LogWarning("Role with ID {RoleId} not found or inactive", request.Id);
                    throw new InvalidOperationException($"Role with ID {request.Id} not found or inactive");
                }

                // Update properties
                existingRole.Name = request.Name;
                existingRole.Description = request.Description;
                existingRole.ModifiedBy = request.ModifiedBy;
                existingRole.ModifiedDate = DateTime.UtcNow;

                // Update the role
                var updatedRole = await _roleRepository.UpdateAsync(existingRole, cancellationToken);

                _logger.LogInformation("Successfully updated role with ID {RoleId}", updatedRole.Id);

                return _mapper.Map<RoleDetailsDto>(updatedRole);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating role with ID {RoleId}", request.Id);
                throw;
            }
        }
    }
}
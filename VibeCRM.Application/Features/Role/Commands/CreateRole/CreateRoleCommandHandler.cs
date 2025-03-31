using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Role;

namespace VibeCRM.Application.Features.Role.Commands.CreateRole
{
    /// <summary>
    /// Handler for processing the CreateRoleCommand
    /// </summary>
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleDetailsDto>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateRoleCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRoleCommandHandler"/> class
        /// </summary>
        /// <param name="roleRepository">The role repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public CreateRoleCommandHandler(
            IRoleRepository roleRepository,
            IMapper mapper,
            ILogger<CreateRoleCommandHandler> logger)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateRoleCommand
        /// </summary>
        /// <param name="request">The command to create a role</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The created role details</returns>
        public async Task<RoleDetailsDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating new role with name {RoleName}", request.Name);

            try
            {
                var role = _mapper.Map<Domain.Entities.BusinessEntities.Role>(request);

                // Set audit properties
                role.CreatedBy = request.CreatedBy;
                role.CreatedDate = DateTime.UtcNow;
                role.ModifiedBy = request.CreatedBy;
                role.ModifiedDate = DateTime.UtcNow;
                role.Active = true;

                var createdRole = await _roleRepository.AddAsync(role, cancellationToken);

                _logger.LogInformation("Successfully created role with ID {RoleId}", createdRole.Id);

                return _mapper.Map<RoleDetailsDto>(createdRole);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating role with name {RoleName}", request.Name);
                throw;
            }
        }
    }
}
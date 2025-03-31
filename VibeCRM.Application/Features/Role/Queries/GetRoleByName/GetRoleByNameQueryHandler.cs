using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Role;

namespace VibeCRM.Application.Features.Role.Queries.GetRoleByName
{
    /// <summary>
    /// Handler for processing the GetRoleByNameQuery
    /// </summary>
    public class GetRoleByNameQueryHandler : IRequestHandler<GetRoleByNameQuery, RoleDetailsDto>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetRoleByNameQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRoleByNameQueryHandler"/> class
        /// </summary>
        /// <param name="roleRepository">The role repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetRoleByNameQueryHandler(
            IRoleRepository roleRepository,
            IMapper mapper,
            ILogger<GetRoleByNameQueryHandler> logger)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetRoleByNameQuery by retrieving a role by its name from the database.
        /// </summary>
        /// <param name="request">The query containing the name of the role to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A RoleDetailsDto representing the requested role, or null if not found.</returns>
        public async Task<RoleDetailsDto> Handle(GetRoleByNameQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving role with name {RoleName}", request.Name);

            try
            {
                var role = await _roleRepository.GetByNameAsync(request.Name, cancellationToken);

                if (role == null || !role.Active)
                {
                    _logger.LogWarning("Role with name {RoleName} not found or inactive", request.Name);
                    return new RoleDetailsDto();
                }

                _logger.LogInformation("Successfully retrieved role with name {RoleName}", role.Name);

                return _mapper.Map<RoleDetailsDto>(role);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving role with name {RoleName}", request.Name);
                throw;
            }
        }
    }
}
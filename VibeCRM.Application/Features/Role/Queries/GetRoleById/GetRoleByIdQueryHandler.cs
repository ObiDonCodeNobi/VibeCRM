using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Role;

namespace VibeCRM.Application.Features.Role.Queries.GetRoleById
{
    /// <summary>
    /// Handler for processing the GetRoleByIdQuery
    /// </summary>
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDetailsDto>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetRoleByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRoleByIdQueryHandler"/> class
        /// </summary>
        /// <param name="roleRepository">The role repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetRoleByIdQueryHandler(
            IRoleRepository roleRepository,
            IMapper mapper,
            ILogger<GetRoleByIdQueryHandler> logger)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetRoleByIdQuery by retrieving a role by its ID from the database.
        /// </summary>
        /// <param name="request">The query containing the ID of the role to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A RoleDetailsDto representing the requested role, or null if not found.</returns>
        public async Task<RoleDetailsDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving role with ID {RoleId}", request.Id);

            try
            {
                var role = await _roleRepository.GetByIdAsync(request.Id, cancellationToken);

                if (role == null || !role.Active)
                {
                    _logger.LogWarning("Role with ID {RoleId} not found or inactive", request.Id);
                    return new RoleDetailsDto();
                }

                _logger.LogInformation("Successfully retrieved role with ID {RoleId}", role.Id);

                return _mapper.Map<RoleDetailsDto>(role);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving role with ID {RoleId}", request.Id);
                throw;
            }
        }
    }
}
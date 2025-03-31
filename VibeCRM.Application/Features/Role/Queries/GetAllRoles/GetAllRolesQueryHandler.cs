using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Role;

namespace VibeCRM.Application.Features.Role.Queries.GetAllRoles
{
    /// <summary>
    /// Handler for processing the GetAllRolesQuery
    /// </summary>
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<RoleListDto>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllRolesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllRolesQueryHandler"/> class
        /// </summary>
        /// <param name="roleRepository">The role repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetAllRolesQueryHandler(
            IRoleRepository roleRepository,
            IMapper mapper,
            ILogger<GetAllRolesQueryHandler> logger)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllRolesQuery
        /// </summary>
        /// <param name="request">The query to retrieve all roles</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collection of all active roles</returns>
        public async Task<IEnumerable<RoleListDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving all active roles");

            try
            {
                var roles = await _roleRepository.GetAllAsync(cancellationToken);

                // Filter to only active roles (though repository should already do this)
                var activeRoles = roles.Where(r => r.Active).ToList();

                _logger.LogInformation("Successfully retrieved {RoleCount} active roles", activeRoles.Count);

                return _mapper.Map<IEnumerable<RoleListDto>>(activeRoles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all active roles");
                throw;
            }
        }
    }
}
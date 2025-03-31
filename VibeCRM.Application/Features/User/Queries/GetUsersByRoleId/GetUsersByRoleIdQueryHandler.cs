using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.User;

namespace VibeCRM.Application.Features.User.Queries.GetUsersByRoleId
{
    /// <summary>
    /// Handler for processing the GetUsersByRoleIdQuery
    /// </summary>
    public class GetUsersByRoleIdQueryHandler : IRequestHandler<GetUsersByRoleIdQuery, IEnumerable<UserListDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetUsersByRoleIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUsersByRoleIdQueryHandler"/> class
        /// </summary>
        /// <param name="userRepository">The user repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetUsersByRoleIdQueryHandler(
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<GetUsersByRoleIdQueryHandler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetUsersByRoleIdQuery
        /// </summary>
        /// <param name="request">The query to retrieve users by role ID</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collection of user list DTOs</returns>
        public async Task<IEnumerable<UserListDto>> Handle(GetUsersByRoleIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving users for role with ID {RoleId}", request.RoleId);

            try
            {
                // Get users by role ID
                // The repository's GetByRoleIdAsync method should already filter by Active = 1
                var users = await _userRepository.GetByRoleIdAsync(request.RoleId, cancellationToken);

                int count = users.Count();
                _logger.LogInformation("Successfully retrieved {Count} users for role with ID {RoleId}", count, request.RoleId);

                var userListDtos = _mapper.Map<IEnumerable<UserListDto>>(users);

                return userListDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving users for role with ID {RoleId}: {ErrorMessage}",
                    request.RoleId, ex.Message);
                throw;
            }
        }
    }
}
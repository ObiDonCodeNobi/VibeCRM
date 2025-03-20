using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.User.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.User.Queries.GetUsersByTeamId
{
    /// <summary>
    /// Handler for processing the GetUsersByTeamIdQuery
    /// </summary>
    public class GetUsersByTeamIdQueryHandler : IRequestHandler<GetUsersByTeamIdQuery, IEnumerable<UserListDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetUsersByTeamIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUsersByTeamIdQueryHandler"/> class
        /// </summary>
        /// <param name="userRepository">The user repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetUsersByTeamIdQueryHandler(
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<GetUsersByTeamIdQueryHandler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetUsersByTeamIdQuery
        /// </summary>
        /// <param name="request">The query to retrieve users by team ID</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collection of user list DTOs</returns>
        public async Task<IEnumerable<UserListDto>> Handle(GetUsersByTeamIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving users for team with ID {TeamId}", request.TeamId);

            try
            {
                // Get users by team ID
                // The repository's GetByTeamIdAsync method should already filter by Active = 1
                var users = await _userRepository.GetByTeamIdAsync(request.TeamId, cancellationToken);

                int count = users.Count();
                _logger.LogInformation("Successfully retrieved {Count} users for team with ID {TeamId}", count, request.TeamId);

                var userListDtos = _mapper.Map<IEnumerable<UserListDto>>(users);

                return userListDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving users for team with ID {TeamId}: {ErrorMessage}",
                    request.TeamId, ex.Message);
                throw;
            }
        }
    }
}
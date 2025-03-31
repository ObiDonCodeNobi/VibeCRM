using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.User;

namespace VibeCRM.Application.Features.User.Queries.GetAllUsers
{
    /// <summary>
    /// Handler for processing the GetAllUsersQuery
    /// </summary>
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserListDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllUsersQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllUsersQueryHandler"/> class
        /// </summary>
        /// <param name="userRepository">The user repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetAllUsersQueryHandler(
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<GetAllUsersQueryHandler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllUsersQuery
        /// </summary>
        /// <param name="request">The query to retrieve all users</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collection of user list DTOs</returns>
        public async Task<IEnumerable<UserListDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving all active users");

            try
            {
                // Get all active users
                // The repository's GetAllAsync method should already filter by Active = 1
                var users = await _userRepository.GetAllAsync(cancellationToken);

                int count = users.Count();
                _logger.LogInformation("Successfully retrieved {Count} active users", count);

                var userListDtos = _mapper.Map<IEnumerable<UserListDto>>(users);

                return userListDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all active users: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.User;

namespace VibeCRM.Application.Features.User.Queries.GetUserByUsername
{
    /// <summary>
    /// Handler for processing the GetUserByUsernameQuery
    /// </summary>
    public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, UserDetailsDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetUserByUsernameQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserByUsernameQueryHandler"/> class
        /// </summary>
        /// <param name="userRepository">The user repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetUserByUsernameQueryHandler(
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<GetUserByUsernameQueryHandler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetUserByUsernameQuery by retrieving a user by their username from the database.
        /// </summary>
        /// <param name="request">The query containing the username of the user to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A UserDetailsDto representing the requested user, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<UserDetailsDto> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving user with username {Username}", request.Username);

            try
            {
                // Get the user by username
                // The repository's GetByUsernameAsync method should already filter by Active = 1
                var user = await _userRepository.GetByUsernameAsync(request.Username, cancellationToken);

                if (user == null || !user.Active)
                {
                    _logger.LogWarning("User with username {Username} not found or inactive", request.Username);
                    return new UserDetailsDto();
                }

                _logger.LogInformation("Successfully retrieved user with username {Username}", request.Username);

                var userDetailsDto = _mapper.Map<UserDetailsDto>(user);

                return userDetailsDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user with username {Username}", request.Username);
                throw;
            }
        }
    }
}
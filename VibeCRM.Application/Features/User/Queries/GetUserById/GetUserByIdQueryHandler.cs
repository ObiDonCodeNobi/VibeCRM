using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.User;

namespace VibeCRM.Application.Features.User.Queries.GetUserById
{
    /// <summary>
    /// Handler for processing the GetUserByIdQuery
    /// </summary>
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDetailsDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetUserByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserByIdQueryHandler"/> class
        /// </summary>
        /// <param name="userRepository">The user repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetUserByIdQueryHandler(
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<GetUserByIdQueryHandler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetUserByIdQuery by retrieving a user by their ID from the database.
        /// </summary>
        /// <param name="request">The query containing the ID of the user to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A UserDetailsDto representing the requested user, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<UserDetailsDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving user with ID {UserId}", request.Id);

            try
            {
                // Get the user by ID
                // The repository's GetByIdAsync method should already filter by Active = 1
                var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

                if (user == null || !user.Active)
                {
                    _logger.LogWarning("User with ID {UserId} not found or inactive", request.Id);
                    return new UserDetailsDto();
                }

                _logger.LogInformation("Successfully retrieved user with ID {UserId}", request.Id);

                var userDetailsDto = _mapper.Map<UserDetailsDto>(user);

                return userDetailsDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user with ID {UserId}", request.Id);
                throw;
            }
        }
    }
}
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.User;

namespace VibeCRM.Application.Features.User.Queries.GetUserByEmail
{
    /// <summary>
    /// Handler for processing the GetUserByEmailQuery
    /// </summary>
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserDetailsDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetUserByEmailQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserByEmailQueryHandler"/> class
        /// </summary>
        /// <param name="userRepository">The user repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetUserByEmailQueryHandler(
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<GetUserByEmailQueryHandler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetUserByEmailQuery by retrieving a user by their email from the database.
        /// </summary>
        /// <param name="request">The query containing the email of the user to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A UserDetailsDto representing the requested user, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<UserDetailsDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving user with email {Email}", request.Email);

            try
            {
                // Get the user by email
                // The repository's GetByEmailAsync method should already filter by Active = 1
                var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

                if (user == null || !user.Active)
                {
                    _logger.LogWarning("User with email {Email} not found or inactive", request.Email);
                    return new UserDetailsDto();
                }

                _logger.LogInformation("Successfully retrieved user with email {Email}", request.Email);

                var userDetailsDto = _mapper.Map<UserDetailsDto>(user);

                return userDetailsDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user with email {Email}", request.Email);
                throw;
            }
        }
    }
}
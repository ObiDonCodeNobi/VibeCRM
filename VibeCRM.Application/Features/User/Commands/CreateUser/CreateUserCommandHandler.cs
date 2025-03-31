using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.User;

namespace VibeCRM.Application.Features.User.Commands.CreateUser
{
    /// <summary>
    /// Handler for processing the CreateUserCommand
    /// </summary>
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDetailsDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUserCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserCommandHandler"/> class
        /// </summary>
        /// <param name="userRepository">The user repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public CreateUserCommandHandler(
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<CreateUserCommandHandler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateUserCommand
        /// </summary>
        /// <param name="request">The command to create a user</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The created user details</returns>
        public async Task<UserDetailsDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating new user with login name {LoginName}", request.LoginName);

            try
            {
                var user = _mapper.Map<Domain.Entities.BusinessEntities.User>(request);

                // Set audit properties
                user.CreatedDate = DateTime.UtcNow;
                user.ModifiedDate = DateTime.UtcNow;

                // Ensure Active is set to true for new entities
                user.Active = true;

                // Set LastLogin to a default value
                user.LastLogin = DateTime.MinValue;

                var createdUser = await _userRepository.AddAsync(user, cancellationToken);

                _logger.LogInformation("Successfully created user with ID {UserId}", createdUser.UserId);

                var userDetailsDto = _mapper.Map<UserDetailsDto>(createdUser);

                return userDetailsDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user with login name {LoginName}", request.LoginName);
                throw;
            }
        }
    }
}
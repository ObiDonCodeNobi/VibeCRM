using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.User.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.User.Commands.UpdateUser
{
    /// <summary>
    /// Handler for processing the UpdateUserCommand
    /// </summary>
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDetailsDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateUserCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserCommandHandler"/> class
        /// </summary>
        /// <param name="userRepository">The user repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public UpdateUserCommandHandler(
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<UpdateUserCommandHandler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateUserCommand
        /// </summary>
        /// <param name="request">The command to update a user</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The updated user details</returns>
        /// <exception cref="InvalidOperationException">Thrown when the user is not found</exception>
        public async Task<UserDetailsDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating user with ID {UserId}", request.Id);

            try
            {
                // Get the existing user
                var existingUser = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingUser == null || !existingUser.Active)
                {
                    _logger.LogWarning("User with ID {UserId} not found or inactive", request.Id);
                    throw new InvalidOperationException($"User with ID {request.Id} not found or inactive");
                }

                // Update properties
                existingUser.PersonId = request.PersonId;
                existingUser.LoginName = request.LoginName;
                existingUser.LoginPassword = request.LoginPassword;
                existingUser.ModifiedDate = DateTime.UtcNow;

                // Update the user
                var updatedUser = await _userRepository.UpdateAsync(existingUser, cancellationToken);

                _logger.LogInformation("Successfully updated user with ID {UserId}", updatedUser.UserId);

                var userDetailsDto = _mapper.Map<UserDetailsDto>(updatedUser);

                return userDetailsDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user with ID {UserId}", request.Id);
                throw;
            }
        }
    }
}
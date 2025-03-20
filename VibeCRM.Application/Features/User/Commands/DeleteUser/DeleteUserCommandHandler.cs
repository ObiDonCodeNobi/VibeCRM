using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.User.Commands.DeleteUser
{
    /// <summary>
    /// Handler for processing the DeleteUserCommand
    /// </summary>
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<DeleteUserCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUserCommandHandler"/> class
        /// </summary>
        /// <param name="userRepository">The user repository</param>
        /// <param name="logger">The logger</param>
        public DeleteUserCommandHandler(
            IUserRepository userRepository,
            ILogger<DeleteUserCommandHandler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteUserCommand
        /// </summary>
        /// <param name="request">The command to delete a user</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>True if the user was successfully deleted; otherwise, false</returns>
        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Soft deleting user with ID {UserId}", request.Id);

            try
            {
                // Get the existing user
                var existingUser = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingUser == null || !existingUser.Active)
                {
                    _logger.LogWarning("User with ID {UserId} not found or already inactive", request.Id);
                    return false;
                }

                // Perform soft delete by setting Active = false
                // The repository's DeleteAsync method handles setting Active = 0
                var result = await _userRepository.DeleteAsync(existingUser.UserId, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Successfully soft deleted user with ID {UserId}", request.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to soft delete user with ID {UserId}", request.Id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error soft deleting user with ID {UserId}", request.Id);
                throw;
            }
        }
    }
}
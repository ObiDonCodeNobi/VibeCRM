using MediatR;
using VibeCRM.Application.Features.User.DTOs;

namespace VibeCRM.Application.Features.User.Commands.CreateUser
{
    /// <summary>
    /// Command for creating a new user
    /// </summary>
    public class CreateUserCommand : IRequest<UserDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the unique identifier of the person associated with this user
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Gets or sets the login name used for authentication
        /// </summary>
        public string LoginName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password used for authentication
        /// </summary>
        /// <remarks>
        /// This should store a hashed version of the password, never the plaintext password.
        /// </remarks>
        public string LoginPassword { get; set; } = string.Empty;
    }
}
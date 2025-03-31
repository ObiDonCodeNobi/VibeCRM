using MediatR;
using VibeCRM.Shared.DTOs.User;

namespace VibeCRM.Application.Features.User.Commands.UpdateUser
{
    /// <summary>
    /// Command for updating an existing user
    /// </summary>
    public class UpdateUserCommand : IRequest<UserDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user
        /// </summary>
        public Guid Id { get; set; }

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
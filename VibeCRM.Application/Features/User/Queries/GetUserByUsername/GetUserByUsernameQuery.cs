using MediatR;
using VibeCRM.Shared.DTOs.User;

namespace VibeCRM.Application.Features.User.Queries.GetUserByUsername
{
    /// <summary>
    /// Query to retrieve a user by username
    /// </summary>
    public class GetUserByUsernameQuery : IRequest<UserDetailsDto>
    {
        /// <summary>
        /// Gets or sets the username of the user to retrieve
        /// </summary>
        public string Username { get; set; } = string.Empty;
    }
}
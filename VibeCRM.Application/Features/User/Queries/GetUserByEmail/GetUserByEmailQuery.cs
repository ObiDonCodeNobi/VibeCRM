using MediatR;
using VibeCRM.Shared.DTOs.User;

namespace VibeCRM.Application.Features.User.Queries.GetUserByEmail
{
    /// <summary>
    /// Query to retrieve a user by email address
    /// </summary>
    public class GetUserByEmailQuery : IRequest<UserDetailsDto>
    {
        /// <summary>
        /// Gets or sets the email address of the user to retrieve
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }
}
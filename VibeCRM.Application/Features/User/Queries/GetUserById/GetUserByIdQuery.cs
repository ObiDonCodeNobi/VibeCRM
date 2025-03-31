using MediatR;
using VibeCRM.Shared.DTOs.User;

namespace VibeCRM.Application.Features.User.Queries.GetUserById
{
    /// <summary>
    /// Query to retrieve a user by ID
    /// </summary>
    public class GetUserByIdQuery : IRequest<UserDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user to retrieve
        /// </summary>
        public Guid Id { get; set; }
    }
}
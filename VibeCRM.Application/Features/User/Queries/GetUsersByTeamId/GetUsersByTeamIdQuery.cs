using MediatR;
using VibeCRM.Application.Features.User.DTOs;

namespace VibeCRM.Application.Features.User.Queries.GetUsersByTeamId
{
    /// <summary>
    /// Query to retrieve users by team ID
    /// </summary>
    public class GetUsersByTeamIdQuery : IRequest<IEnumerable<UserListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the team to retrieve users for
        /// </summary>
        public Guid TeamId { get; set; }
    }
}
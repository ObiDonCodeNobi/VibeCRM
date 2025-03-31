using MediatR;
using VibeCRM.Shared.DTOs.Team;

namespace VibeCRM.Application.Features.Team.Queries.GetTeamsByUserId
{
    /// <summary>
    /// Query to retrieve all teams that a user is a member of
    /// </summary>
    public class GetTeamsByUserIdQuery : IRequest<IEnumerable<TeamListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user
        /// </summary>
        public Guid UserId { get; set; }
    }
}
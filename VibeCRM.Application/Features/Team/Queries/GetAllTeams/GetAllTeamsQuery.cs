using MediatR;
using VibeCRM.Shared.DTOs.Team;

namespace VibeCRM.Application.Features.Team.Queries.GetAllTeams
{
    /// <summary>
    /// Query to retrieve all teams
    /// </summary>
    public class GetAllTeamsQuery : IRequest<IEnumerable<TeamListDto>>
    {
        // This query doesn't require any parameters
    }
}
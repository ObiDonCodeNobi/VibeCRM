using MediatR;
using VibeCRM.Application.Features.Team.DTOs;

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
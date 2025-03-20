using MediatR;
using VibeCRM.Application.Features.Team.DTOs;

namespace VibeCRM.Application.Features.Team.Queries.GetTeamByName
{
    /// <summary>
    /// Query to retrieve a team by its name
    /// </summary>
    public class GetTeamByNameQuery : IRequest<TeamDetailsDto>
    {
        /// <summary>
        /// Gets or sets the name of the team to retrieve
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
using MediatR;
using VibeCRM.Application.Features.Team.DTOs;

namespace VibeCRM.Application.Features.Team.Queries.GetTeamById
{
    /// <summary>
    /// Query to retrieve a team by its ID
    /// </summary>
    public class GetTeamByIdQuery : IRequest<TeamDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the team to retrieve
        /// </summary>
        public Guid Id { get; set; }
    }
}
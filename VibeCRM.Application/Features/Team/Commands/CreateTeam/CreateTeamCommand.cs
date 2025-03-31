using MediatR;
using VibeCRM.Shared.DTOs.Team;

namespace VibeCRM.Application.Features.Team.Commands.CreateTeam
{
    /// <summary>
    /// Command for creating a new team
    /// </summary>
    public class CreateTeamCommand : IRequest<TeamDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the team
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the unique identifier of the employee who leads this team
        /// </summary>
        public Guid TeamLeadEmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the team
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the detailed description of the team
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
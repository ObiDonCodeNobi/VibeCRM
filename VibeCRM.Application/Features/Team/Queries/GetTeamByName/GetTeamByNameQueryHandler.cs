using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Team;

namespace VibeCRM.Application.Features.Team.Queries.GetTeamByName
{
    /// <summary>
    /// Handler for processing the GetTeamByNameQuery
    /// </summary>
    public class GetTeamByNameQueryHandler : IRequestHandler<GetTeamByNameQuery, TeamDetailsDto>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetTeamByNameQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTeamByNameQueryHandler"/> class
        /// </summary>
        /// <param name="teamRepository">The team repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetTeamByNameQueryHandler(
            ITeamRepository teamRepository,
            IMapper mapper,
            ILogger<GetTeamByNameQueryHandler> logger)
        {
            _teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetTeamByNameQuery by retrieving a team by its name from the database.
        /// </summary>
        /// <param name="request">The query containing the name of the team to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A TeamDetailsDto representing the requested team, or null if not found.</returns>
        public async Task<TeamDetailsDto> Handle(GetTeamByNameQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving team with name {TeamName}", request.Name);

            try
            {
                var team = await _teamRepository.GetByNameAsync(request.Name, cancellationToken);

                if (team == null || !team.Active)
                {
                    _logger.LogWarning("Team with name {TeamName} not found", request.Name);
                    return new TeamDetailsDto();
                }

                var teamDetailsDto = _mapper.Map<TeamDetailsDto>(team);

                // Note: In a real implementation, we would need to get the actual member count
                // For now, we'll leave it as 0 since we don't have the repository method to get it
                teamDetailsDto.MemberCount = 0;

                _logger.LogInformation("Successfully retrieved team with name {TeamName}", request.Name);

                return teamDetailsDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving team with name {TeamName}", request.Name);
                throw;
            }
        }
    }
}
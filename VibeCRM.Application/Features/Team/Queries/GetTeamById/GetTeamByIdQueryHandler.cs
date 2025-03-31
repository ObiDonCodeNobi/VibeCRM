using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Team;

namespace VibeCRM.Application.Features.Team.Queries.GetTeamById
{
    /// <summary>
    /// Handler for processing the GetTeamByIdQuery
    /// </summary>
    public class GetTeamByIdQueryHandler : IRequestHandler<GetTeamByIdQuery, TeamDetailsDto>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetTeamByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTeamByIdQueryHandler"/> class
        /// </summary>
        /// <param name="teamRepository">The team repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetTeamByIdQueryHandler(
            ITeamRepository teamRepository,
            IMapper mapper,
            ILogger<GetTeamByIdQueryHandler> logger)
        {
            _teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetTeamByIdQuery by retrieving a team by its ID from the database.
        /// </summary>
        /// <param name="request">The query containing the ID of the team to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A TeamDetailsDto representing the requested team, or null if not found.</returns>
        public async Task<TeamDetailsDto> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving team with ID {TeamId}", request.Id);

            try
            {
                var team = await _teamRepository.GetByIdAsync(request.Id, cancellationToken);

                if (team == null || !team.Active)
                {
                    _logger.LogWarning("Team with ID {TeamId} not found", request.Id);
                    return new TeamDetailsDto();
                }

                var teamDetailsDto = _mapper.Map<TeamDetailsDto>(team);

                // Note: In a real implementation, we would need to get the actual member count
                // For now, we'll leave it as 0 since we don't have the repository method to get it
                teamDetailsDto.MemberCount = 0;

                _logger.LogInformation("Successfully retrieved team with ID {TeamId}", request.Id);

                return teamDetailsDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving team with ID {TeamId}", request.Id);
                throw;
            }
        }
    }
}
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Team.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Team.Queries.GetAllTeams
{
    /// <summary>
    /// Handler for processing the GetAllTeamsQuery
    /// </summary>
    public class GetAllTeamsQueryHandler : IRequestHandler<GetAllTeamsQuery, IEnumerable<TeamListDto>>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllTeamsQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllTeamsQueryHandler"/> class
        /// </summary>
        /// <param name="teamRepository">The team repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetAllTeamsQueryHandler(
            ITeamRepository teamRepository,
            IMapper mapper,
            ILogger<GetAllTeamsQueryHandler> logger)
        {
            _teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllTeamsQuery
        /// </summary>
        /// <param name="request">The query to retrieve all teams</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collection of team list DTOs</returns>
        public async Task<IEnumerable<TeamListDto>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving all teams");

            try
            {
                var teams = await _teamRepository.GetAllAsync(cancellationToken);
                var teamListDtos = _mapper.Map<IEnumerable<TeamListDto>>(teams);

                // Note: In a real implementation, we would need to get the actual member count for each team
                // For now, we'll leave it as 0 since we don't have the repository method to get it

                _logger.LogInformation("Successfully retrieved all teams");

                return teamListDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all teams");
                throw;
            }
        }
    }
}
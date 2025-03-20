using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Team.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Team.Queries.GetTeamsByUserId
{
    /// <summary>
    /// Handler for processing the GetTeamsByUserIdQuery
    /// </summary>
    public class GetTeamsByUserIdQueryHandler : IRequestHandler<GetTeamsByUserIdQuery, IEnumerable<TeamListDto>>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetTeamsByUserIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTeamsByUserIdQueryHandler"/> class
        /// </summary>
        /// <param name="teamRepository">The team repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetTeamsByUserIdQueryHandler(
            ITeamRepository teamRepository,
            IMapper mapper,
            ILogger<GetTeamsByUserIdQueryHandler> logger)
        {
            _teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetTeamsByUserIdQuery
        /// </summary>
        /// <param name="request">The query to retrieve teams by user ID</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collection of team list DTOs</returns>
        public async Task<IEnumerable<TeamListDto>> Handle(GetTeamsByUserIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving teams for user with ID {UserId}", request.UserId);

            try
            {
                var teams = await _teamRepository.GetByUserIdAsync(request.UserId, cancellationToken);
                var teamListDtos = _mapper.Map<IEnumerable<TeamListDto>>(teams);

                // Note: In a real implementation, we would need to get the actual member count for each team
                // For now, we'll leave it as 0 since we don't have the repository method to get it

                _logger.LogInformation("Successfully retrieved teams for user with ID {UserId}", request.UserId);

                return teamListDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving teams for user with ID {UserId}", request.UserId);
                throw;
            }
        }
    }
}
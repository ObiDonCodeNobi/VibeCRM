using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Team;

namespace VibeCRM.Application.Features.Team.Commands.CreateTeam
{
    /// <summary>
    /// Handler for processing the CreateTeamCommand
    /// </summary>
    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, TeamDetailsDto>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateTeamCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTeamCommandHandler"/> class
        /// </summary>
        /// <param name="teamRepository">The team repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public CreateTeamCommandHandler(
            ITeamRepository teamRepository,
            IMapper mapper,
            ILogger<CreateTeamCommandHandler> logger)
        {
            _teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateTeamCommand
        /// </summary>
        /// <param name="request">The command to create a team</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The created team details</returns>
        public async Task<TeamDetailsDto> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating new team with name {TeamName}", request.Name);

            try
            {
                var team = _mapper.Map<Domain.Entities.BusinessEntities.Team>(request);

                // Set audit properties
                team.CreatedDate = DateTime.UtcNow;
                team.ModifiedDate = DateTime.UtcNow;

                // Ensure Active is set to true for new entities
                team.Active = true;

                var createdTeam = await _teamRepository.AddAsync(team, cancellationToken);

                _logger.LogInformation("Successfully created team with ID {TeamId}", createdTeam.Id);

                var teamDetailsDto = _mapper.Map<TeamDetailsDto>(createdTeam);

                // Set MemberCount to 0 for a newly created team
                teamDetailsDto.MemberCount = 0;

                return teamDetailsDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating team with name {TeamName}", request.Name);
                throw;
            }
        }
    }
}
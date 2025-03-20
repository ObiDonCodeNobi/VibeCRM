using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Team.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Team.Commands.UpdateTeam
{
    /// <summary>
    /// Handler for processing the UpdateTeamCommand
    /// </summary>
    public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, TeamDetailsDto>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateTeamCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTeamCommandHandler"/> class
        /// </summary>
        /// <param name="teamRepository">The team repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public UpdateTeamCommandHandler(
            ITeamRepository teamRepository,
            IMapper mapper,
            ILogger<UpdateTeamCommandHandler> logger)
        {
            _teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateTeamCommand
        /// </summary>
        /// <param name="request">The command to update a team</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The updated team details</returns>
        /// <exception cref="ArgumentException">Thrown when the team with the specified ID is not found</exception>
        public async Task<TeamDetailsDto> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating team with ID {TeamId}", request.Id);

            try
            {
                // Check if the team exists
                var existingTeam = await _teamRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingTeam == null || !existingTeam.Active)
                {
                    _logger.LogWarning("Team with ID {TeamId} not found", request.Id);
                    throw new ArgumentException($"Team with ID {request.Id} not found", nameof(request.Id));
                }

                // Map the updated properties while preserving existing ones
                _mapper.Map(request, existingTeam);

                // Update audit properties
                existingTeam.ModifiedDate = DateTime.UtcNow;

                var updatedTeam = await _teamRepository.UpdateAsync(existingTeam, cancellationToken);

                _logger.LogInformation("Successfully updated team with ID {TeamId}", updatedTeam.Id);

                // Get the member count for the team
                var teamDetailsDto = _mapper.Map<TeamDetailsDto>(updatedTeam);

                // Note: In a real implementation, we would need to get the actual member count
                // For now, we'll leave it as 0 since we don't have the repository method to get it
                teamDetailsDto.MemberCount = 0;

                return teamDetailsDto;
            }
            catch (Exception ex) when (!(ex is ArgumentException))
            {
                _logger.LogError(ex, "Error updating team with ID {TeamId}", request.Id);
                throw;
            }
        }
    }
}
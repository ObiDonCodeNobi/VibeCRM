using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Team.Commands.DeleteTeam
{
    /// <summary>
    /// Handler for processing the DeleteTeamCommand
    /// </summary>
    public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, bool>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ILogger<DeleteTeamCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteTeamCommandHandler"/> class
        /// </summary>
        /// <param name="teamRepository">The team repository</param>
        /// <param name="logger">The logger</param>
        public DeleteTeamCommandHandler(
            ITeamRepository teamRepository,
            ILogger<DeleteTeamCommandHandler> logger)
        {
            _teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteTeamCommand
        /// </summary>
        /// <param name="request">The command to delete a team</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>True if the team was successfully deleted, otherwise false</returns>
        public async Task<bool> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting team with ID {TeamId}", request.Id);

            try
            {
                // Check if the team exists
                var existingTeam = await _teamRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingTeam == null || !existingTeam.Active)
                {
                    _logger.LogWarning("Team with ID {TeamId} not found", request.Id);
                    return false;
                }

                // Perform soft delete by setting Active = false
                var result = await _teamRepository.DeleteAsync(request.Id, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Successfully deleted team with ID {TeamId}", request.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to delete team with ID {TeamId}", request.Id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting team with ID {TeamId}", request.Id);
                throw;
            }
        }
    }
}
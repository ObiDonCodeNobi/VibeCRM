using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.State.Commands.DeleteState
{
    /// <summary>
    /// Handler for the DeleteStateCommand.
    /// Handles the deletion of an existing state.
    /// </summary>
    public class DeleteStateCommandHandler : IRequestHandler<DeleteStateCommand, bool>
    {
        private readonly IStateRepository _stateRepository;
        private readonly ILogger<DeleteStateCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteStateCommandHandler"/> class.
        /// </summary>
        /// <param name="stateRepository">The state repository.</param>
        /// <param name="logger">The logger.</param>
        public DeleteStateCommandHandler(
            IStateRepository stateRepository,
            ILogger<DeleteStateCommandHandler> logger)
        {
            _stateRepository = stateRepository ?? throw new ArgumentNullException(nameof(stateRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteStateCommand by soft-deleting an existing state.
        /// </summary>
        /// <param name="request">The command for deleting an existing state.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the state was successfully deleted; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the deletion process.</exception>
        public async Task<bool> Handle(DeleteStateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Deleting state with ID: {StateId}", request.Id);

                // Check if the state exists
                var existingState = await _stateRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingState == null)
                {
                    _logger.LogWarning("State with ID: {StateId} not found", request.Id);
                    return false;
                }

                // Delete from repository (soft delete)
                var result = await _stateRepository.DeleteAsync(request.Id, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Successfully deleted state with ID: {StateId}", request.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to delete state with ID: {StateId}", request.Id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting state with ID {StateId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
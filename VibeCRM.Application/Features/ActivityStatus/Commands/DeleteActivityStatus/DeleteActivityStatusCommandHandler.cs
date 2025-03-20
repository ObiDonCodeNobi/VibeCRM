using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ActivityStatus.Commands.DeleteActivityStatus
{
    /// <summary>
    /// Handler for the DeleteActivityStatusCommand.
    /// Performs a soft delete of an activity status by setting Active = false.
    /// </summary>
    public class DeleteActivityStatusCommandHandler : IRequestHandler<DeleteActivityStatusCommand, bool>
    {
        private readonly IActivityStatusRepository _activityStatusRepository;
        private readonly ILogger<DeleteActivityStatusCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteActivityStatusCommandHandler"/> class.
        /// </summary>
        /// <param name="activityStatusRepository">The activity status repository.</param>
        /// <param name="logger">The logger.</param>
        public DeleteActivityStatusCommandHandler(
            IActivityStatusRepository activityStatusRepository,
            ILogger<DeleteActivityStatusCommandHandler> logger)
        {
            _activityStatusRepository = activityStatusRepository ?? throw new ArgumentNullException(nameof(activityStatusRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteActivityStatusCommand by performing a soft delete of an activity status.
        /// </summary>
        /// <param name="request">The command containing the ID of the activity status to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the activity status was deleted successfully; otherwise, false.</returns>
        public async Task<bool> Handle(DeleteActivityStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Soft deleting activity status with ID: {ActivityStatusId}", request.Id);

                // Check if activity status exists
                var existingActivityStatus = await _activityStatusRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingActivityStatus == null)
                {
                    _logger.LogWarning("Activity status with ID {ActivityStatusId} not found", request.Id);
                    return false;
                }

                // Update ModifiedDate before soft delete
                existingActivityStatus.ModifiedDate = DateTime.UtcNow;
                await _activityStatusRepository.UpdateAsync(existingActivityStatus, cancellationToken);

                // Perform soft delete (sets Active = false)
                var result = await _activityStatusRepository.DeleteAsync(request.Id, cancellationToken);

                _logger.LogInformation("Successfully soft deleted activity status with ID: {ActivityStatusId}", request.Id);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error soft deleting activity status with ID {ActivityStatusId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
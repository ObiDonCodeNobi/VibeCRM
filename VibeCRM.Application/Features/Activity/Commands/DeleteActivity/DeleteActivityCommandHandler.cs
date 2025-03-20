using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Activity.Commands.DeleteActivity
{
    /// <summary>
    /// Handler for processing DeleteActivityCommand requests.
    /// Implements the CQRS command handler pattern for soft deleting activity entities.
    /// </summary>
    public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand, bool>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ILogger<DeleteActivityCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteActivityCommandHandler"/> class.
        /// </summary>
        /// <param name="activityRepository">The activity repository for database operations.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public DeleteActivityCommandHandler(
            IActivityRepository activityRepository,
            ILogger<DeleteActivityCommandHandler> logger)
        {
            _activityRepository = activityRepository ?? throw new ArgumentNullException(nameof(activityRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteActivityCommand by soft deleting an activity entity in the database.
        /// The soft delete operation sets the Active property to false rather than removing the record.
        /// </summary>
        /// <param name="request">The DeleteActivityCommand containing the activity ID to soft delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>True if the activity was successfully soft deleted, false if it was not found or already inactive.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the activity ID is empty.</exception>
        public async Task<bool> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.ActivityId == Guid.Empty) throw new ArgumentException("Activity ID cannot be empty", nameof(request.ActivityId));

            try
            {
                // Get the existing activity (Active=1 filter is applied in the repository)
                var activity = await _activityRepository.GetByIdAsync(request.ActivityId, cancellationToken);

                if (activity == null)
                {
                    _logger.LogWarning("Activity with ID {ActivityId} not found or is already inactive", request.ActivityId);
                    return false;
                }

                // Set the ModifiedBy property before soft deleting
                activity.ModifiedBy = request.ModifiedBy;
                activity.ModifiedDate = DateTime.UtcNow;

                // Perform soft delete (sets Active = false)
                await _activityRepository.DeleteAsync(activity.Id, cancellationToken);
                _logger.LogInformation("Soft deleted activity with ID: {ActivityId}", request.ActivityId);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while soft deleting activity with ID {ActivityId}: {ErrorMessage}",
                    request.ActivityId, ex.Message);
                throw;
            }
        }
    }
}
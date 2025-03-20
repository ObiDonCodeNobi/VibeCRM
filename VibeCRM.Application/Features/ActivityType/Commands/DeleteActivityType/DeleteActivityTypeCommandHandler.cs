using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ActivityType.Commands.DeleteActivityType
{
    /// <summary>
    /// Handler for the DeleteActivityTypeCommand.
    /// Performs a soft delete of an activity type by setting the Active property to false.
    /// </summary>
    public class DeleteActivityTypeCommandHandler : IRequestHandler<DeleteActivityTypeCommand, bool>
    {
        private readonly IActivityTypeRepository _activityTypeRepository;
        private readonly ILogger<DeleteActivityTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteActivityTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="activityTypeRepository">The activity type repository.</param>
        /// <param name="logger">The logger.</param>
        public DeleteActivityTypeCommandHandler(
            IActivityTypeRepository activityTypeRepository,
            ILogger<DeleteActivityTypeCommandHandler> logger)
        {
            _activityTypeRepository = activityTypeRepository ?? throw new ArgumentNullException(nameof(activityTypeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteActivityTypeCommand by performing a soft delete of an activity type.
        /// </summary>
        /// <param name="request">The command containing the ID of the activity type to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the activity type was successfully deleted; otherwise, false.</returns>
        public async Task<bool> Handle(DeleteActivityTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Soft deleting activity type with ID: {ActivityTypeId}", request.Id);

                // Check if activity type exists
                var activityType = await _activityTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (activityType == null)
                {
                    _logger.LogWarning("Activity type with ID {ActivityTypeId} not found", request.Id);
                    return false;
                }

                // Perform soft delete (sets Active = false)
                var result = await _activityTypeRepository.DeleteAsync(request.Id, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Successfully soft deleted activity type with ID: {ActivityTypeId}", request.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to soft delete activity type with ID: {ActivityTypeId}", request.Id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error soft deleting activity type with ID {ActivityTypeId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
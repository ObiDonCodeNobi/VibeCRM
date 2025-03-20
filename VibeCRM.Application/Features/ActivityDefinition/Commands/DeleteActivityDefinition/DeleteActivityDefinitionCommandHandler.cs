using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.ActivityDefinition.Commands.DeleteActivityDefinition
{
    /// <summary>
    /// Handler for processing DeleteActivityDefinitionCommand requests.
    /// Implements the CQRS command handler pattern for soft-deleting activity definition entities.
    /// </summary>
    public class DeleteActivityDefinitionCommandHandler : IRequestHandler<DeleteActivityDefinitionCommand, bool>
    {
        private readonly IActivityDefinitionRepository _activityDefinitionRepository;
        private readonly ILogger<DeleteActivityDefinitionCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteActivityDefinitionCommandHandler"/> class.
        /// </summary>
        /// <param name="activityDefinitionRepository">The activity definition repository for database operations.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public DeleteActivityDefinitionCommandHandler(
            IActivityDefinitionRepository activityDefinitionRepository,
            ILogger<DeleteActivityDefinitionCommandHandler> logger)
        {
            _activityDefinitionRepository = activityDefinitionRepository ?? throw new ArgumentNullException(nameof(activityDefinitionRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteActivityDefinitionCommand by soft-deleting an activity definition entity in the database.
        /// Sets the Active property to false rather than physically removing the record.
        /// </summary>
        /// <param name="request">The DeleteActivityDefinitionCommand containing the activity definition ID to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>True if the activity definition was successfully soft-deleted, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the activity definition ID is empty.</exception>
        public async Task<bool> Handle(DeleteActivityDefinitionCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.ActivityDefinitionId == Guid.Empty) throw new ArgumentException("Activity definition ID cannot be empty", nameof(request.ActivityDefinitionId));
            if (request.ModifiedBy == Guid.Empty) throw new ArgumentException("Modified by user ID cannot be empty", nameof(request.ModifiedBy));

            try
            {
                // Get the existing activity definition
                var existingActivityDefinition = await _activityDefinitionRepository.GetByIdAsync(request.ActivityDefinitionId, cancellationToken);
                if (existingActivityDefinition == null)
                {
                    _logger.LogWarning("Activity definition with ID {ActivityDefinitionId} not found or is already inactive", request.ActivityDefinitionId);
                    return false;
                }

                // Update the ModifiedBy property before deletion
                existingActivityDefinition.ModifiedBy = request.ModifiedBy;
                existingActivityDefinition.ModifiedDate = DateTime.UtcNow;

                // First update the activity definition with the modified information
                await _activityDefinitionRepository.UpdateAsync(existingActivityDefinition, cancellationToken);

                // Then soft delete the activity definition (sets Active = false)
                var result = await _activityDefinitionRepository.DeleteAsync(request.ActivityDefinitionId, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Soft-deleted activity definition with ID: {ActivityDefinitionId}", request.ActivityDefinitionId);
                }
                else
                {
                    _logger.LogWarning("Failed to soft-delete activity definition with ID: {ActivityDefinitionId}", request.ActivityDefinitionId);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while soft-deleting activity definition with ID: {ActivityDefinitionId}", request.ActivityDefinitionId);
                throw;
            }
        }
    }
}
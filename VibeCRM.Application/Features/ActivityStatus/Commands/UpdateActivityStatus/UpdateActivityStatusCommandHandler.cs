using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ActivityStatus.Commands.UpdateActivityStatus
{
    /// <summary>
    /// Handler for the UpdateActivityStatusCommand.
    /// Updates an existing activity status in the system.
    /// </summary>
    public class UpdateActivityStatusCommandHandler : IRequestHandler<UpdateActivityStatusCommand, bool>
    {
        private readonly IActivityStatusRepository _activityStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateActivityStatusCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateActivityStatusCommandHandler"/> class.
        /// </summary>
        /// <param name="activityStatusRepository">The activity status repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public UpdateActivityStatusCommandHandler(
            IActivityStatusRepository activityStatusRepository,
            IMapper mapper,
            ILogger<UpdateActivityStatusCommandHandler> logger)
        {
            _activityStatusRepository = activityStatusRepository ?? throw new ArgumentNullException(nameof(activityStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateActivityStatusCommand by updating an existing activity status.
        /// </summary>
        /// <param name="request">The command containing the updated activity status details.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the activity status was updated successfully; otherwise, false.</returns>
        public async Task<bool> Handle(UpdateActivityStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating activity status with ID: {ActivityStatusId}", request.Id);

                // Check if activity status exists
                var existingActivityStatus = await _activityStatusRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingActivityStatus == null)
                {
                    _logger.LogWarning("Activity status with ID {ActivityStatusId} not found", request.Id);
                    return false;
                }

                // Update properties
                existingActivityStatus.Status = request.Status;
                existingActivityStatus.Description = request.Description;
                existingActivityStatus.OrdinalPosition = request.OrdinalPosition;
                existingActivityStatus.ModifiedDate = DateTime.UtcNow;

                // Save to repository
                await _activityStatusRepository.UpdateAsync(existingActivityStatus, cancellationToken);

                _logger.LogInformation("Successfully updated activity status with ID: {ActivityStatusId}", request.Id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating activity status with ID {ActivityStatusId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
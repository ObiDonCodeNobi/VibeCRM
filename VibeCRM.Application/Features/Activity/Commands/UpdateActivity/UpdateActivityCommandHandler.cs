using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Common.Exceptions;
using VibeCRM.Application.Features.Activity.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Activity.Commands.UpdateActivity
{
    /// <summary>
    /// Handler for processing UpdateActivityCommand requests.
    /// Implements the CQRS command handler pattern for updating existing activity entities.
    /// </summary>
    public class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, ActivityDetailsDto>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateActivityCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateActivityCommandHandler"/> class.
        /// </summary>
        /// <param name="activityRepository">The activity repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public UpdateActivityCommandHandler(
            IActivityRepository activityRepository,
            IMapper mapper,
            ILogger<UpdateActivityCommandHandler> logger)
        {
            _activityRepository = activityRepository ?? throw new ArgumentNullException(nameof(activityRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateActivityCommand by updating an existing activity entity in the database.
        /// </summary>
        /// <param name="request">The UpdateActivityCommand containing the updated activity details.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>An ActivityDetailsDto representing the updated activity, or null if the activity was not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the activity ID is empty.</exception>
        public async Task<ActivityDetailsDto> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.ActivityId == Guid.Empty) throw new ArgumentException("Activity ID cannot be empty", nameof(request.ActivityId));

            try
            {
                // Get the existing activity (Active=1 filter is applied in the repository)
                var activity = await _activityRepository.GetByIdAsync(request.ActivityId, cancellationToken);

                if (activity == null)
                {
                    _logger.LogWarning("Activity with ID {ActivityId} not found or is inactive", request.ActivityId);
                    throw new NotFoundException(nameof(Domain.Entities.BusinessEntities.Activity), request.ActivityId);
                }

                // Map request to entity, preserving the original ID and Active status
                _mapper.Map(request, activity);

                // Update the activity in the repository
                var updatedActivity = await _activityRepository.UpdateAsync(activity, cancellationToken);
                _logger.LogInformation("Updated activity with ID: {ActivityId}", updatedActivity.Id);

                // Return the mapped DTO
                return _mapper.Map<ActivityDetailsDto>(updatedActivity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating activity with ID {ActivityId}: {ErrorMessage}",
                    request.ActivityId, ex.Message);
                throw;
            }
        }
    }
}
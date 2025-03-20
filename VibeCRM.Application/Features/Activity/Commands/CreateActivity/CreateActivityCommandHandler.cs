using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Activity.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Activity.Commands.CreateActivity
{
    /// <summary>
    /// Handler for processing CreateActivityCommand requests.
    /// Implements the CQRS command handler pattern for creating new activity entities.
    /// </summary>
    public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, ActivityDetailsDto>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateActivityCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateActivityCommandHandler"/> class.
        /// </summary>
        /// <param name="activityRepository">The activity repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public CreateActivityCommandHandler(
            IActivityRepository activityRepository,
            IMapper mapper,
            ILogger<CreateActivityCommandHandler> logger)
        {
            _activityRepository = activityRepository ?? throw new ArgumentNullException(nameof(activityRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateActivityCommand by creating a new activity entity in the database.
        /// </summary>
        /// <param name="request">The CreateActivityCommand containing the activity details.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>An ActivityDetailsDto representing the newly created activity.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when validation fails.</exception>
        public async Task<ActivityDetailsDto> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                // Map command to entity
                var activity = _mapper.Map<VibeCRM.Domain.Entities.BusinessEntities.Activity>(request);

                // Add the activity to the repository
                var createdActivity = await _activityRepository.AddAsync(activity, cancellationToken);
                _logger.LogInformation("Created new activity with ID: {ActivityId}", createdActivity.Id);

                // Return the mapped DTO
                return _mapper.Map<ActivityDetailsDto>(createdActivity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating activity: {Subject}", request.Subject);
                throw;
            }
        }
    }
}
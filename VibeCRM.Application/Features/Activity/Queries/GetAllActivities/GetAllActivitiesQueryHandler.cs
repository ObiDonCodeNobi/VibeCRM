using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Activity;

namespace VibeCRM.Application.Features.Activity.Queries.GetAllActivities
{
    /// <summary>
    /// Handler for processing GetAllActivitiesQuery requests.
    /// Implements the CQRS query handler pattern for retrieving all activities.
    /// </summary>
    public class GetAllActivitiesQueryHandler : IRequestHandler<GetAllActivitiesQuery, List<ActivityListDto>>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllActivitiesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllActivitiesQueryHandler"/> class.
        /// </summary>
        /// <param name="activityRepository">The activity repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetAllActivitiesQueryHandler(
            IActivityRepository activityRepository,
            IMapper mapper,
            ILogger<GetAllActivitiesQueryHandler> logger)
        {
            _activityRepository = activityRepository ?? throw new ArgumentNullException(nameof(activityRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllActivitiesQuery by retrieving all active activities from the database.
        /// </summary>
        /// <param name="request">The GetAllActivitiesQuery request object.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A list of ActivityListDto objects representing all active activities.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<List<ActivityListDto>> Handle(GetAllActivitiesQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                // Get all active activities ordered by due date
                var activities = await _activityRepository.GetAllAsync(cancellationToken);
                _logger.LogInformation("Retrieved {Count} activities", activities.Count());

                // Map entities to DTOs
                return _mapper.Map<List<ActivityListDto>>(activities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all activities: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Activity.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Activity.Queries.GetActivityById
{
    /// <summary>
    /// Handler for processing GetActivityByIdQuery requests.
    /// Implements the CQRS query handler pattern for retrieving a specific activity by ID.
    /// </summary>
    public class GetActivityByIdQueryHandler : IRequestHandler<GetActivityByIdQuery, ActivityDetailsDto?>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetActivityByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetActivityByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="activityRepository">The activity repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetActivityByIdQueryHandler(
            IActivityRepository activityRepository,
            IMapper mapper,
            ILogger<GetActivityByIdQueryHandler> logger)
        {
            _activityRepository = activityRepository ?? throw new ArgumentNullException(nameof(activityRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetActivityByIdQuery by retrieving a specific activity from the database.
        /// </summary>
        /// <param name="request">The GetActivityByIdQuery containing the activity ID to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>An ActivityDetailsDto representing the requested activity, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the activity ID is empty.</exception>
        public async Task<ActivityDetailsDto?> Handle(GetActivityByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Id == Guid.Empty) throw new ArgumentException("Activity ID cannot be empty", nameof(request.Id));

            try
            {
                // Get the activity by ID (Active=1 filter is applied in the repository)
                var activity = await _activityRepository.GetByIdAsync(request.Id, cancellationToken);

                if (activity == null)
                {
                    _logger.LogWarning("Activity with ID {ActivityId} not found or is inactive", request.Id);
                    return null;
                }

                _logger.LogInformation("Retrieved activity with ID: {ActivityId}", request.Id);

                // Map entity to DTO
                return _mapper.Map<ActivityDetailsDto>(activity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving activity with ID {ActivityId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
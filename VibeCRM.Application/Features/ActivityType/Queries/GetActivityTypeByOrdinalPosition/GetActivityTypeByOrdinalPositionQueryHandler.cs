using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.ActivityType.DTOs;
using VibeCRM.Domain.Exceptions;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ActivityType.Queries.GetActivityTypeByOrdinalPosition
{
    /// <summary>
    /// Handler for the GetActivityTypeByOrdinalPositionQuery.
    /// Retrieves an activity type by its ordinal position.
    /// </summary>
    public class GetActivityTypeByOrdinalPositionQueryHandler : IRequestHandler<GetActivityTypeByOrdinalPositionQuery, ActivityTypeDto>
    {
        private readonly IActivityTypeRepository _activityTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetActivityTypeByOrdinalPositionQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetActivityTypeByOrdinalPositionQueryHandler"/> class.
        /// </summary>
        /// <param name="activityTypeRepository">The activity type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetActivityTypeByOrdinalPositionQueryHandler(
            IActivityTypeRepository activityTypeRepository,
            IMapper mapper,
            ILogger<GetActivityTypeByOrdinalPositionQueryHandler> logger)
        {
            _activityTypeRepository = activityTypeRepository ?? throw new ArgumentNullException(nameof(activityTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetActivityTypeByOrdinalPositionQuery by retrieving an activity type by its ordinal position.
        /// </summary>
        /// <param name="request">The query containing the ordinal position to search for.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The activity type DTO with the specified ordinal position, or null if not found.</returns>
        public async Task<ActivityTypeDto> Handle(GetActivityTypeByOrdinalPositionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving activity type with ordinal position: {OrdinalPosition}", request.OrdinalPosition);

                // Get all activity types ordered by ordinal position
                var activityTypes = await _activityTypeRepository.GetByOrdinalPositionAsync(cancellationToken);

                // Find the one with the requested ordinal position
                var activityType = activityTypes.FirstOrDefault(at => at.OrdinalPosition == request.OrdinalPosition);

                if (activityType == null)
                {
                    _logger.LogWarning("Activity type with ordinal position {OrdinalPosition} not found", request.OrdinalPosition);
                    throw new EntityNotFoundException($"Activity type with ordinal position {request.OrdinalPosition} not found.");
                }

                // Map to DTO
                var activityTypeDto = _mapper.Map<ActivityTypeDto>(activityType);

                _logger.LogInformation("Successfully retrieved activity type with ID: {ActivityTypeId} for ordinal position: {OrdinalPosition}",
                    activityTypeDto.Id, request.OrdinalPosition);

                return activityTypeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving activity type with ordinal position {OrdinalPosition}: {ErrorMessage}",
                    request.OrdinalPosition, ex.Message);
                throw;
            }
        }
    }
}
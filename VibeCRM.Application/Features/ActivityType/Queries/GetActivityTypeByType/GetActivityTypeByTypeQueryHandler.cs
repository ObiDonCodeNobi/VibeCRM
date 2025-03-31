using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.ActivityType;

namespace VibeCRM.Application.Features.ActivityType.Queries.GetActivityTypeByType
{
    /// <summary>
    /// Handler for the GetActivityTypeByTypeQuery.
    /// Retrieves a specific activity type by its type name.
    /// </summary>
    public class GetActivityTypeByTypeQueryHandler : IRequestHandler<GetActivityTypeByTypeQuery, ActivityTypeDetailsDto?>
    {
        private readonly IActivityTypeRepository _activityTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetActivityTypeByTypeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetActivityTypeByTypeQueryHandler"/> class.
        /// </summary>
        /// <param name="activityTypeRepository">The activity type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetActivityTypeByTypeQueryHandler(
            IActivityTypeRepository activityTypeRepository,
            IMapper mapper,
            ILogger<GetActivityTypeByTypeQueryHandler> logger)
        {
            _activityTypeRepository = activityTypeRepository ?? throw new ArgumentNullException(nameof(activityTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetActivityTypeByTypeQuery by retrieving a specific activity type by its type name.
        /// </summary>
        /// <param name="request">The query containing the type name of the activity type to retrieve.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The activity type details DTO if found; otherwise, null.</returns>
        public async Task<ActivityTypeDetailsDto?> Handle(GetActivityTypeByTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving activity type with type name: {ActivityTypeName}", request.Type);

                var activityType = await _activityTypeRepository.GetByTypeAsync(request.Type, cancellationToken);
                if (activityType == null)
                {
                    _logger.LogWarning("Activity type with type name {ActivityTypeName} not found", request.Type);
                    return null;
                }

                var activityTypeDto = _mapper.Map<ActivityTypeDetailsDto>(activityType);
                activityTypeDto.ActivityCount = 0;

                _logger.LogInformation("Successfully retrieved activity type with type name: {ActivityTypeName}", request.Type);

                return activityTypeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving activity type with type name {ActivityTypeName}: {ErrorMessage}", request.Type, ex.Message);
                throw;
            }
        }
    }
}
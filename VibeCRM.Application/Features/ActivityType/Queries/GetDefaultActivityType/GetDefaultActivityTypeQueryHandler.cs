using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.ActivityType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ActivityType.Queries.GetDefaultActivityType
{
    /// <summary>
    /// Handler for the GetDefaultActivityTypeQuery.
    /// Retrieves the default activity type, which is typically the one with the lowest ordinal position.
    /// </summary>
    public class GetDefaultActivityTypeQueryHandler : IRequestHandler<GetDefaultActivityTypeQuery, ActivityTypeDto?>
    {
        private readonly IActivityTypeRepository _activityTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDefaultActivityTypeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultActivityTypeQueryHandler"/> class.
        /// </summary>
        /// <param name="activityTypeRepository">The activity type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetDefaultActivityTypeQueryHandler(
            IActivityTypeRepository activityTypeRepository,
            IMapper mapper,
            ILogger<GetDefaultActivityTypeQueryHandler> logger)
        {
            _activityTypeRepository = activityTypeRepository ?? throw new ArgumentNullException(nameof(activityTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetDefaultActivityTypeQuery by retrieving the default activity type.
        /// </summary>
        /// <param name="request">The query to retrieve the default activity type.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The default activity type DTO if found; otherwise, null.</returns>
        public async Task<ActivityTypeDto?> Handle(GetDefaultActivityTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving default activity type");

                var activityTypes = await _activityTypeRepository.GetAllAsync(cancellationToken);
                if (activityTypes == null || !activityTypes.Any())
                {
                    _logger.LogWarning("No activity types found");
                    return null;
                }

                var defaultActivityType = activityTypes.OrderBy(x => x.OrdinalPosition).FirstOrDefault();
                if (defaultActivityType == null)
                {
                    _logger.LogWarning("No default activity type found");
                    return null;
                }

                var activityTypeDto = _mapper.Map<ActivityTypeDto>(defaultActivityType);

                _logger.LogInformation("Successfully retrieved default activity type with ID: {ActivityTypeId}", activityTypeDto.Id);

                return activityTypeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving default activity type: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
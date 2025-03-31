using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.ActivityType;

namespace VibeCRM.Application.Features.ActivityType.Queries.GetAllActivityTypes
{
    /// <summary>
    /// Handler for the GetAllActivityTypesQuery.
    /// Retrieves all activity types.
    /// </summary>
    public class GetAllActivityTypesQueryHandler : IRequestHandler<GetAllActivityTypesQuery, IEnumerable<ActivityTypeListDto>>
    {
        private readonly IActivityTypeRepository _activityTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllActivityTypesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllActivityTypesQueryHandler"/> class.
        /// </summary>
        /// <param name="activityTypeRepository">The activity type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetAllActivityTypesQueryHandler(
            IActivityTypeRepository activityTypeRepository,
            IMapper mapper,
            ILogger<GetAllActivityTypesQueryHandler> logger)
        {
            _activityTypeRepository = activityTypeRepository ?? throw new ArgumentNullException(nameof(activityTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllActivityTypesQuery by retrieving all activity types.
        /// </summary>
        /// <param name="request">The query to retrieve all activity types.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of activity type list DTOs.</returns>
        public async Task<IEnumerable<ActivityTypeListDto>> Handle(GetAllActivityTypesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving all activity types");

                // Get all activity types from repository
                var activityTypes = await _activityTypeRepository.GetAllAsync(cancellationToken);

                // Map to DTOs
                var activityTypeDtos = _mapper.Map<IEnumerable<ActivityTypeListDto>>(activityTypes);

                // Set activity counts to 0 for now
                // In a real implementation, you would retrieve the actual activity counts
                foreach (var dto in activityTypeDtos)
                {
                    dto.ActivityCount = 0;
                }

                _logger.LogInformation("Successfully retrieved all activity types");

                return activityTypeDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all activity types: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
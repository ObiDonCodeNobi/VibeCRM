using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.ActivityStatus;

namespace VibeCRM.Application.Features.ActivityStatus.Queries.GetActivityStatusByOrdinalPosition
{
    /// <summary>
    /// Handler for the GetActivityStatusByOrdinalPositionQuery.
    /// Retrieves activity statuses ordered by ordinal position.
    /// </summary>
    public class GetActivityStatusByOrdinalPositionQueryHandler : IRequestHandler<GetActivityStatusByOrdinalPositionQuery, IEnumerable<ActivityStatusListDto>>
    {
        private readonly IActivityStatusRepository _activityStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetActivityStatusByOrdinalPositionQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetActivityStatusByOrdinalPositionQueryHandler"/> class.
        /// </summary>
        /// <param name="activityStatusRepository">The activity status repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetActivityStatusByOrdinalPositionQueryHandler(
            IActivityStatusRepository activityStatusRepository,
            IMapper mapper,
            ILogger<GetActivityStatusByOrdinalPositionQueryHandler> logger)
        {
            _activityStatusRepository = activityStatusRepository ?? throw new ArgumentNullException(nameof(activityStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetActivityStatusByOrdinalPositionQuery by retrieving activity statuses ordered by ordinal position.
        /// </summary>
        /// <param name="request">The query to retrieve activity statuses ordered by ordinal position.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of activity status list DTOs ordered by ordinal position.</returns>
        public async Task<IEnumerable<ActivityStatusListDto>> Handle(GetActivityStatusByOrdinalPositionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving activity statuses ordered by ordinal position");

                // Get activity statuses from repository
                var activityStatuses = await _activityStatusRepository.GetByOrdinalPositionAsync(cancellationToken);

                // Map to DTOs
                var activityStatusDtos = _mapper.Map<IEnumerable<ActivityStatusListDto>>(activityStatuses);

                // Set activity count to 0 for now
                // In a real implementation, you would retrieve the actual activity counts
                foreach (var dto in activityStatusDtos)
                {
                    dto.ActivityCount = 0;
                }

                _logger.LogInformation("Successfully retrieved activity statuses ordered by ordinal position");

                return activityStatusDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving activity statuses ordered by ordinal position: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
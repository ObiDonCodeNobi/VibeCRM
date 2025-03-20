using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.ActivityStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ActivityStatus.Queries.GetAllActivityStatuses
{
    /// <summary>
    /// Handler for the GetAllActivityStatusesQuery.
    /// Retrieves all activity statuses in the system.
    /// </summary>
    public class GetAllActivityStatusesQueryHandler : IRequestHandler<GetAllActivityStatusesQuery, IEnumerable<ActivityStatusListDto>>
    {
        private readonly IActivityStatusRepository _activityStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllActivityStatusesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllActivityStatusesQueryHandler"/> class.
        /// </summary>
        /// <param name="activityStatusRepository">The activity status repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetAllActivityStatusesQueryHandler(
            IActivityStatusRepository activityStatusRepository,
            IMapper mapper,
            ILogger<GetAllActivityStatusesQueryHandler> logger)
        {
            _activityStatusRepository = activityStatusRepository ?? throw new ArgumentNullException(nameof(activityStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllActivityStatusesQuery by retrieving all activity statuses.
        /// </summary>
        /// <param name="request">The query to retrieve all activity statuses.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of activity status list DTOs.</returns>
        public async Task<IEnumerable<ActivityStatusListDto>> Handle(GetAllActivityStatusesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving all activity statuses");

                // Get all activity statuses from repository
                var activityStatuses = await _activityStatusRepository.GetAllAsync(cancellationToken);

                // Map to DTOs
                var activityStatusDtos = _mapper.Map<IEnumerable<ActivityStatusListDto>>(activityStatuses);

                // Set activity count to 0 for now
                // In a real implementation, you would retrieve the actual activity counts
                foreach (var dto in activityStatusDtos)
                {
                    dto.ActivityCount = 0;
                }

                _logger.LogInformation("Successfully retrieved all activity statuses");

                return activityStatusDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all activity statuses: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
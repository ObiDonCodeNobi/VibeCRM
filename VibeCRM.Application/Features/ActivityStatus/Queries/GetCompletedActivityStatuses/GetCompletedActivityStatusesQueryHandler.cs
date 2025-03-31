using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.ActivityStatus;

namespace VibeCRM.Application.Features.ActivityStatus.Queries.GetCompletedActivityStatuses
{
    /// <summary>
    /// Handler for the GetCompletedActivityStatusesQuery.
    /// Retrieves all completed activity statuses in the system.
    /// </summary>
    public class GetCompletedActivityStatusesQueryHandler : IRequestHandler<GetCompletedActivityStatusesQuery, IEnumerable<ActivityStatusListDto>>
    {
        private readonly IActivityStatusRepository _activityStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCompletedActivityStatusesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCompletedActivityStatusesQueryHandler"/> class.
        /// </summary>
        /// <param name="activityStatusRepository">The activity status repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetCompletedActivityStatusesQueryHandler(
            IActivityStatusRepository activityStatusRepository,
            IMapper mapper,
            ILogger<GetCompletedActivityStatusesQueryHandler> logger)
        {
            _activityStatusRepository = activityStatusRepository ?? throw new ArgumentNullException(nameof(activityStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetCompletedActivityStatusesQuery by retrieving all completed activity statuses.
        /// </summary>
        /// <param name="request">The query to retrieve all completed activity statuses.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of completed activity status list DTOs.</returns>
        public async Task<IEnumerable<ActivityStatusListDto>> Handle(GetCompletedActivityStatusesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving all completed activity statuses");

                // Get completed activity statuses from repository
                var completedActivityStatuses = await _activityStatusRepository.GetCompletedStatusesAsync(cancellationToken);

                // Map to DTOs
                var activityStatusDtos = _mapper.Map<IEnumerable<ActivityStatusListDto>>(completedActivityStatuses);

                // Set activity count to 0 for now
                // In a real implementation, you would retrieve the actual activity counts
                foreach (var dto in activityStatusDtos)
                {
                    dto.ActivityCount = 0;
                }

                _logger.LogInformation("Successfully retrieved all completed activity statuses");

                return activityStatusDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving completed activity statuses: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
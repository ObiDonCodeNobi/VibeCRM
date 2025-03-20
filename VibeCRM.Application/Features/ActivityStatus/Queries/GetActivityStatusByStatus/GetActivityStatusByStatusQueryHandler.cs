using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.ActivityStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ActivityStatus.Queries.GetActivityStatusByStatus
{
    /// <summary>
    /// Handler for the GetActivityStatusByStatusQuery.
    /// Retrieves activity statuses by status name.
    /// </summary>
    public class GetActivityStatusByStatusQueryHandler : IRequestHandler<GetActivityStatusByStatusQuery, IEnumerable<ActivityStatusListDto>>
    {
        private readonly IActivityStatusRepository _activityStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetActivityStatusByStatusQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetActivityStatusByStatusQueryHandler"/> class.
        /// </summary>
        /// <param name="activityStatusRepository">The activity status repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetActivityStatusByStatusQueryHandler(
            IActivityStatusRepository activityStatusRepository,
            IMapper mapper,
            ILogger<GetActivityStatusByStatusQueryHandler> logger)
        {
            _activityStatusRepository = activityStatusRepository ?? throw new ArgumentNullException(nameof(activityStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetActivityStatusByStatusQuery by retrieving activity statuses by status name.
        /// </summary>
        /// <param name="request">The query containing the status name to search for.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of activity status list DTOs matching the status name.</returns>
        public async Task<IEnumerable<ActivityStatusListDto>> Handle(GetActivityStatusByStatusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving activity statuses with status name: {StatusName}", request.Status);

                // Get activity statuses from repository
                var activityStatuses = await _activityStatusRepository.GetByStatusAsync(request.Status, cancellationToken);

                // Map to DTOs
                var activityStatusDtos = _mapper.Map<IEnumerable<ActivityStatusListDto>>(activityStatuses);

                // Set activity count to 0 for now
                // In a real implementation, you would retrieve the actual activity counts
                foreach (var dto in activityStatusDtos)
                {
                    dto.ActivityCount = 0;
                }

                _logger.LogInformation("Successfully retrieved activity statuses with status name: {StatusName}", request.Status);

                return activityStatusDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving activity statuses with status name {StatusName}: {ErrorMessage}", request.Status, ex.Message);
                throw;
            }
        }
    }
}
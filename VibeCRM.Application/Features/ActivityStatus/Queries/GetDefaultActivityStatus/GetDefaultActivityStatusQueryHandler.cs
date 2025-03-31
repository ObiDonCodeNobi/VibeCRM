using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Common.Exceptions;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.ActivityStatus;

namespace VibeCRM.Application.Features.ActivityStatus.Queries.GetDefaultActivityStatus
{
    /// <summary>
    /// Handler for the GetDefaultActivityStatusQuery.
    /// Retrieves the default activity status in the system.
    /// </summary>
    public class GetDefaultActivityStatusQueryHandler : IRequestHandler<GetDefaultActivityStatusQuery, ActivityStatusDto>
    {
        private readonly IActivityStatusRepository _activityStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDefaultActivityStatusQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultActivityStatusQueryHandler"/> class.
        /// </summary>
        /// <param name="activityStatusRepository">The activity status repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetDefaultActivityStatusQueryHandler(
            IActivityStatusRepository activityStatusRepository,
            IMapper mapper,
            ILogger<GetDefaultActivityStatusQueryHandler> logger)
        {
            _activityStatusRepository = activityStatusRepository ?? throw new ArgumentNullException(nameof(activityStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetDefaultActivityStatusQuery by retrieving the default activity status.
        /// </summary>
        /// <param name="request">The query to retrieve the default activity status.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The default activity status DTO if found; otherwise, throws an exception.</returns>
        public async Task<ActivityStatusDto> Handle(GetDefaultActivityStatusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving default activity status");

                // Get default activity status from repository
                var defaultActivityStatus = await _activityStatusRepository.GetDefaultAsync(cancellationToken);
                if (defaultActivityStatus == null)
                {
                    _logger.LogWarning("Default activity status not found");
                    throw new NotFoundException("Default activity status not found.");
                }

                // Map to DTO
                var activityStatusDto = _mapper.Map<ActivityStatusDto>(defaultActivityStatus);

                _logger.LogInformation("Successfully retrieved default activity status with ID: {ActivityStatusId}", defaultActivityStatus.Id);

                return activityStatusDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving default activity status: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
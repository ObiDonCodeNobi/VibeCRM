using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Common.Exceptions;
using VibeCRM.Application.Features.ActivityStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ActivityStatus.Queries.GetActivityStatusById
{
    /// <summary>
    /// Handler for the GetActivityStatusByIdQuery.
    /// Retrieves a specific activity status by its ID.
    /// </summary>
    public class GetActivityStatusByIdQueryHandler : IRequestHandler<GetActivityStatusByIdQuery, ActivityStatusDetailsDto>
    {
        private readonly IActivityStatusRepository _activityStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetActivityStatusByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetActivityStatusByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="activityStatusRepository">The activity status repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetActivityStatusByIdQueryHandler(
            IActivityStatusRepository activityStatusRepository,
            IMapper mapper,
            ILogger<GetActivityStatusByIdQueryHandler> logger)
        {
            _activityStatusRepository = activityStatusRepository ?? throw new ArgumentNullException(nameof(activityStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetActivityStatusByIdQuery by retrieving a specific activity status by its ID.
        /// </summary>
        /// <param name="request">The query containing the ID of the activity status to retrieve.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The activity status details DTO if found; otherwise, throws an exception.</returns>
        public async Task<ActivityStatusDetailsDto> Handle(GetActivityStatusByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving activity status with ID: {ActivityStatusId}", request.Id);

                // Get activity status from repository
                var activityStatus = await _activityStatusRepository.GetByIdAsync(request.Id, cancellationToken);
                if (activityStatus == null)
                {
                    _logger.LogWarning("Activity status with ID {ActivityStatusId} not found", request.Id);
                    throw new NotFoundException($"Activity status with ID {request.Id} not found.");
                }

                // Map to DTO
                var activityStatusDto = _mapper.Map<ActivityStatusDetailsDto>(activityStatus);

                // Set activity count to 0 for now
                // In a real implementation, you would retrieve the actual activity count
                activityStatusDto.ActivityCount = 0;

                _logger.LogInformation("Successfully retrieved activity status with ID: {ActivityStatusId}", request.Id);

                return activityStatusDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving activity status with ID {ActivityStatusId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
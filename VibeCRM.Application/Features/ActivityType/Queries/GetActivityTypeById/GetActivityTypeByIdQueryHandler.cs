using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Common.Exceptions;
using VibeCRM.Application.Features.ActivityType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ActivityType.Queries.GetActivityTypeById
{
    /// <summary>
    /// Handler for the GetActivityTypeByIdQuery.
    /// Retrieves a specific activity type by its ID.
    /// </summary>
    public class GetActivityTypeByIdQueryHandler : IRequestHandler<GetActivityTypeByIdQuery, ActivityTypeDetailsDto>
    {
        private readonly IActivityTypeRepository _activityTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetActivityTypeByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetActivityTypeByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="activityTypeRepository">The activity type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetActivityTypeByIdQueryHandler(
            IActivityTypeRepository activityTypeRepository,
            IMapper mapper,
            ILogger<GetActivityTypeByIdQueryHandler> logger)
        {
            _activityTypeRepository = activityTypeRepository ?? throw new ArgumentNullException(nameof(activityTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetActivityTypeByIdQuery by retrieving a specific activity type by its ID.
        /// </summary>
        /// <param name="request">The query containing the ID of the activity type to retrieve.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The activity type details DTO if found; otherwise, throws an exception.</returns>
        public async Task<ActivityTypeDetailsDto> Handle(GetActivityTypeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving activity type with ID: {ActivityTypeId}", request.Id);

                // Get activity type from repository
                var activityType = await _activityTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (activityType == null)
                {
                    _logger.LogWarning("Activity type with ID {ActivityTypeId} not found", request.Id);
                    throw new NotFoundException($"Activity type with ID {request.Id} not found.");
                }

                // Map to DTO
                var activityTypeDto = _mapper.Map<ActivityTypeDetailsDto>(activityType);

                // Set activity count to 0 for now
                // In a real implementation, you would retrieve the actual activity count
                activityTypeDto.ActivityCount = 0;

                _logger.LogInformation("Successfully retrieved activity type with ID: {ActivityTypeId}", request.Id);

                return activityTypeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving activity type with ID {ActivityTypeId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
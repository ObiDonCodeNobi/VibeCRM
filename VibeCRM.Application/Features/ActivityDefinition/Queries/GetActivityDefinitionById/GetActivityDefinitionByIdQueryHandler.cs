using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Common.Exceptions;
using VibeCRM.Application.Features.ActivityDefinition.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.ActivityDefinition.Queries.GetActivityDefinitionById
{
    /// <summary>
    /// Handler for processing GetActivityDefinitionByIdQuery requests.
    /// Implements the CQRS query handler pattern for retrieving a specific activity definition.
    /// </summary>
    public class GetActivityDefinitionByIdQueryHandler : IRequestHandler<GetActivityDefinitionByIdQuery, ActivityDefinitionDetailsDto>
    {
        private readonly IActivityDefinitionRepository _activityDefinitionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetActivityDefinitionByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetActivityDefinitionByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="activityDefinitionRepository">The activity definition repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetActivityDefinitionByIdQueryHandler(
            IActivityDefinitionRepository activityDefinitionRepository,
            IMapper mapper,
            ILogger<GetActivityDefinitionByIdQueryHandler> logger)
        {
            _activityDefinitionRepository = activityDefinitionRepository ?? throw new ArgumentNullException(nameof(activityDefinitionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetActivityDefinitionByIdQuery by retrieving a specific activity definition from the database.
        /// </summary>
        /// <param name="request">The GetActivityDefinitionByIdQuery containing the activity definition ID to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>An ActivityDefinitionDetailsDto representing the retrieved activity definition.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the activity definition ID is empty.</exception>
        /// <exception cref="NotFoundException">Thrown when the activity definition is not found or is inactive.</exception>
        public async Task<ActivityDefinitionDetailsDto> Handle(GetActivityDefinitionByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.ActivityDefinitionId == Guid.Empty) throw new ArgumentException("Activity definition ID cannot be empty", nameof(request.ActivityDefinitionId));

            try
            {
                // Get the activity definition by ID
                var activityDefinition = await _activityDefinitionRepository.GetByIdAsync(request.ActivityDefinitionId, cancellationToken);

                // Return exception if not found or not active
                if (activityDefinition == null || !activityDefinition.Active)
                {
                    _logger.LogWarning("Activity definition with ID {ActivityDefinitionId} not found or is inactive", request.ActivityDefinitionId);
                    throw new NotFoundException($"Activity definition with ID {request.ActivityDefinitionId} not found or is inactive.");
                }

                _logger.LogInformation("Retrieved activity definition with ID: {ActivityDefinitionId}", activityDefinition.ActivityDefinitionId);

                // Map entity to DTO
                var activityDefinitionDto = _mapper.Map<ActivityDefinitionDetailsDto>(activityDefinition);

                // Note: In a real implementation, you would also populate the related entity names
                // (ActivityTypeName, ActivityStatusName, etc.) by fetching them from their respective repositories

                return activityDefinitionDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving activity definition with ID {ActivityDefinitionId}: {ErrorMessage}", request.ActivityDefinitionId, ex.Message);
                throw;
            }
        }
    }
}
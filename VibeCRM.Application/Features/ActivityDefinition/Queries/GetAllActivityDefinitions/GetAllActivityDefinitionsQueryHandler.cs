using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.ActivityDefinition;

namespace VibeCRM.Application.Features.ActivityDefinition.Queries.GetAllActivityDefinitions
{
    /// <summary>
    /// Handler for processing GetAllActivityDefinitionsQuery requests.
    /// Implements the CQRS query handler pattern for retrieving all activity definitions.
    /// </summary>
    public class GetAllActivityDefinitionsQueryHandler : IRequestHandler<GetAllActivityDefinitionsQuery, List<ActivityDefinitionListDto>>
    {
        private readonly IActivityDefinitionRepository _activityDefinitionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllActivityDefinitionsQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllActivityDefinitionsQueryHandler"/> class.
        /// </summary>
        /// <param name="activityDefinitionRepository">The activity definition repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetAllActivityDefinitionsQueryHandler(
            IActivityDefinitionRepository activityDefinitionRepository,
            IMapper mapper,
            ILogger<GetAllActivityDefinitionsQueryHandler> logger)
        {
            _activityDefinitionRepository = activityDefinitionRepository ?? throw new ArgumentNullException(nameof(activityDefinitionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllActivityDefinitionsQuery by retrieving all active activity definitions from the database.
        /// </summary>
        /// <param name="request">The GetAllActivityDefinitionsQuery request object.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A list of ActivityDefinitionListDto objects representing all active activity definitions.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<List<ActivityDefinitionListDto>> Handle(GetAllActivityDefinitionsQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                // Get all activity definitions
                var activityDefinitions = await _activityDefinitionRepository.GetAllAsync(cancellationToken);

                // Filter to only include active activity definitions
                var activeActivityDefinitions = activityDefinitions.Where(a => a.Active);

                _logger.LogInformation("Retrieved {Count} active activity definitions", activeActivityDefinitions.Count());

                // Map entities to DTOs
                return _mapper.Map<List<ActivityDefinitionListDto>>(activeActivityDefinitions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all activity definitions: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
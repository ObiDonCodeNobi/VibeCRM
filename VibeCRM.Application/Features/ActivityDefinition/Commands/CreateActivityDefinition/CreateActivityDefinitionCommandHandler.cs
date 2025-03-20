using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.ActivityDefinition.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.ActivityDefinition.Commands.CreateActivityDefinition
{
    /// <summary>
    /// Handler for processing CreateActivityDefinitionCommand requests.
    /// Implements the CQRS command handler pattern for creating activity definition entities.
    /// </summary>
    public class CreateActivityDefinitionCommandHandler : IRequestHandler<CreateActivityDefinitionCommand, ActivityDefinitionDetailsDto>
    {
        private readonly IActivityDefinitionRepository _activityDefinitionRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateActivityDefinitionCommand> _validator;
        private readonly ILogger<CreateActivityDefinitionCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateActivityDefinitionCommandHandler"/> class.
        /// </summary>
        /// <param name="activityDefinitionRepository">The activity definition repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="validator">The validator for the command.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public CreateActivityDefinitionCommandHandler(
            IActivityDefinitionRepository activityDefinitionRepository,
            IMapper mapper,
            IValidator<CreateActivityDefinitionCommand> validator,
            ILogger<CreateActivityDefinitionCommandHandler> logger)
        {
            _activityDefinitionRepository = activityDefinitionRepository ?? throw new ArgumentNullException(nameof(activityDefinitionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateActivityDefinitionCommand by creating a new activity definition entity in the database.
        /// </summary>
        /// <param name="request">The CreateActivityDefinitionCommand containing the activity definition data.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>An ActivityDefinitionDetailsDto representing the newly created activity definition.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ValidationException">Thrown when the command fails validation.</exception>
        public async Task<ActivityDefinitionDetailsDto> Handle(CreateActivityDefinitionCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            // Validate the command
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for CreateActivityDefinitionCommand: {Errors}", string.Join(", ", validationResult.Errors));
                throw new ValidationException(validationResult.Errors);
            }

            try
            {
                // Set creation and modification dates
                var now = DateTime.UtcNow;

                // Map command to entity
                var activityDefinition = _mapper.Map<Domain.Entities.BusinessEntities.ActivityDefinition>(request);
                activityDefinition.CreatedDate = now;
                activityDefinition.ModifiedDate = now;
                activityDefinition.Active = true;

                // Add to repository
                var createdActivityDefinition = await _activityDefinitionRepository.AddAsync(activityDefinition, cancellationToken);
                _logger.LogInformation("Created activity definition with ID: {ActivityDefinitionId}", createdActivityDefinition.ActivityDefinitionId);

                // Map entity to DTO
                var activityDefinitionDto = _mapper.Map<ActivityDefinitionDetailsDto>(createdActivityDefinition);

                // Note: In a real implementation, you would also populate the related entity names
                // (ActivityTypeName, ActivityStatusName, etc.) by fetching them from their respective repositories

                return activityDefinitionDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating activity definition: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
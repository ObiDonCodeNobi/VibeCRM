using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.ActivityDefinition.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.ActivityDefinition.Commands.UpdateActivityDefinition
{
    /// <summary>
    /// Handler for processing UpdateActivityDefinitionCommand requests.
    /// Implements the CQRS command handler pattern for updating activity definition entities.
    /// </summary>
    public class UpdateActivityDefinitionCommandHandler : IRequestHandler<UpdateActivityDefinitionCommand, ActivityDefinitionDetailsDto>
    {
        private readonly IActivityDefinitionRepository _activityDefinitionRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateActivityDefinitionCommand> _validator;
        private readonly ILogger<UpdateActivityDefinitionCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateActivityDefinitionCommandHandler"/> class.
        /// </summary>
        /// <param name="activityDefinitionRepository">The activity definition repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="validator">The validator for the command.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public UpdateActivityDefinitionCommandHandler(
            IActivityDefinitionRepository activityDefinitionRepository,
            IMapper mapper,
            IValidator<UpdateActivityDefinitionCommand> validator,
            ILogger<UpdateActivityDefinitionCommandHandler> logger)
        {
            _activityDefinitionRepository = activityDefinitionRepository ?? throw new ArgumentNullException(nameof(activityDefinitionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateActivityDefinitionCommand by updating an existing activity definition entity in the database.
        /// </summary>
        /// <param name="request">The UpdateActivityDefinitionCommand containing the updated activity definition data.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>An ActivityDefinitionDetailsDto representing the updated activity definition.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ValidationException">Thrown when the command fails validation.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the activity definition to update is not found.</exception>
        public async Task<ActivityDefinitionDetailsDto> Handle(UpdateActivityDefinitionCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            // Validate the command
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for UpdateActivityDefinitionCommand: {Errors}", string.Join(", ", validationResult.Errors));
                throw new ValidationException(validationResult.Errors);
            }

            try
            {
                // Get the existing activity definition
                var existingActivityDefinition = await _activityDefinitionRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingActivityDefinition == null)
                {
                    _logger.LogWarning("Activity definition with ID {ActivityDefinitionId} not found", request.Id);
                    throw new InvalidOperationException($"Activity definition with ID {request.Id} not found");
                }

                // Update the entity with values from the command
                _mapper.Map(request, existingActivityDefinition);
                existingActivityDefinition.ModifiedDate = DateTime.UtcNow;

                // Update in repository
                var updatedActivityDefinition = await _activityDefinitionRepository.UpdateAsync(existingActivityDefinition, cancellationToken);
                _logger.LogInformation("Updated activity definition with ID: {ActivityDefinitionId}", updatedActivityDefinition.ActivityDefinitionId);

                // Map entity to DTO
                var activityDefinitionDto = _mapper.Map<ActivityDefinitionDetailsDto>(updatedActivityDefinition);

                // Note: In a real implementation, you would also populate the related entity names
                // (ActivityTypeName, ActivityStatusName, etc.) by fetching them from their respective repositories

                return activityDefinitionDto;
            }
            catch (Exception ex) when (ex is not InvalidOperationException && ex is not ValidationException)
            {
                _logger.LogError(ex, "Error updating activity definition with ID {ActivityDefinitionId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
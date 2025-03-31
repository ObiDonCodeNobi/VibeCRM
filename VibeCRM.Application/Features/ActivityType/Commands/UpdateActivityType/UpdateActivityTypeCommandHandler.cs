using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.ActivityType;

namespace VibeCRM.Application.Features.ActivityType.Commands.UpdateActivityType
{
    /// <summary>
    /// Handler for the UpdateActivityTypeCommand.
    /// Updates an existing activity type in the database.
    /// </summary>
    public class UpdateActivityTypeCommandHandler : IRequestHandler<UpdateActivityTypeCommand, ActivityTypeDto>
    {
        private readonly IActivityTypeRepository _activityTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateActivityTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateActivityTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="activityTypeRepository">The activity type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public UpdateActivityTypeCommandHandler(
            IActivityTypeRepository activityTypeRepository,
            IMapper mapper,
            ILogger<UpdateActivityTypeCommandHandler> logger)
        {
            _activityTypeRepository = activityTypeRepository ?? throw new ArgumentNullException(nameof(activityTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateActivityTypeCommand by updating an existing activity type.
        /// </summary>
        /// <param name="request">The command containing the activity type details to update.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated activity type DTO.</returns>
        public async Task<ActivityTypeDto> Handle(UpdateActivityTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating activity type with ID: {ActivityTypeId}", request.Id);

                // Get existing entity
                var existingActivityType = await _activityTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingActivityType == null)
                {
                    _logger.LogWarning("Activity type with ID {ActivityTypeId} not found", request.Id);
                    throw new Exception($"Activity type with ID {request.Id} not found");
                }

                // Update properties
                existingActivityType.Type = request.Type;
                existingActivityType.Description = request.Description;
                existingActivityType.OrdinalPosition = request.OrdinalPosition;
                existingActivityType.ModifiedDate = DateTime.UtcNow;
                existingActivityType.ModifiedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"); // This should be replaced with the actual user ID

                // Update in repository
                var updatedActivityType = await _activityTypeRepository.UpdateAsync(existingActivityType, cancellationToken);

                // Map to DTO
                var activityTypeDto = _mapper.Map<ActivityTypeDto>(updatedActivityType);

                _logger.LogInformation("Successfully updated activity type with ID: {ActivityTypeId}", activityTypeDto.Id);

                return activityTypeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating activity type with ID {ActivityTypeId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
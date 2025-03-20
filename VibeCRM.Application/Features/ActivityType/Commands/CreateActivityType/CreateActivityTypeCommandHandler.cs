using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.ActivityType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ActivityType.Commands.CreateActivityType
{
    /// <summary>
    /// Handler for the CreateActivityTypeCommand.
    /// Creates a new activity type in the database.
    /// </summary>
    public class CreateActivityTypeCommandHandler : IRequestHandler<CreateActivityTypeCommand, ActivityTypeDto>
    {
        private readonly IActivityTypeRepository _activityTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateActivityTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateActivityTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="activityTypeRepository">The activity type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public CreateActivityTypeCommandHandler(
            IActivityTypeRepository activityTypeRepository,
            IMapper mapper,
            ILogger<CreateActivityTypeCommandHandler> logger)
        {
            _activityTypeRepository = activityTypeRepository ?? throw new ArgumentNullException(nameof(activityTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateActivityTypeCommand by creating a new activity type.
        /// </summary>
        /// <param name="request">The command containing the activity type details to create.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created activity type DTO.</returns>
        public async Task<ActivityTypeDto> Handle(CreateActivityTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating new activity type with type: {ActivityType}", request.Type);

                // Map command to entity
                var activityTypeEntity = _mapper.Map<Domain.Entities.TypeStatusEntities.ActivityType>(request);

                // Set audit fields
                activityTypeEntity.Id = Guid.NewGuid();
                activityTypeEntity.CreatedDate = DateTime.UtcNow;
                activityTypeEntity.CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"); // This should be replaced with the actual user ID
                activityTypeEntity.ModifiedDate = DateTime.UtcNow;
                activityTypeEntity.ModifiedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"); // This should be replaced with the actual user ID
                activityTypeEntity.Active = true;

                // Add to repository
                var createdActivityType = await _activityTypeRepository.AddAsync(activityTypeEntity, cancellationToken);

                // Map to DTO
                var activityTypeDto = _mapper.Map<ActivityTypeDto>(createdActivityType);

                _logger.LogInformation("Successfully created activity type with ID: {ActivityTypeId}", activityTypeDto.Id);

                return activityTypeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating activity type: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
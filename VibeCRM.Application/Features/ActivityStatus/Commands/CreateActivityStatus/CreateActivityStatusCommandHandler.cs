using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ActivityStatus.Commands.CreateActivityStatus
{
    /// <summary>
    /// Handler for the CreateActivityStatusCommand.
    /// Creates a new activity status in the system.
    /// </summary>
    public class CreateActivityStatusCommandHandler : IRequestHandler<CreateActivityStatusCommand, Guid>
    {
        private readonly IActivityStatusRepository _activityStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateActivityStatusCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateActivityStatusCommandHandler"/> class.
        /// </summary>
        /// <param name="activityStatusRepository">The activity status repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public CreateActivityStatusCommandHandler(
            IActivityStatusRepository activityStatusRepository,
            IMapper mapper,
            ILogger<CreateActivityStatusCommandHandler> logger)
        {
            _activityStatusRepository = activityStatusRepository ?? throw new ArgumentNullException(nameof(activityStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateActivityStatusCommand by creating a new activity status.
        /// </summary>
        /// <param name="request">The command containing the activity status details.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The ID of the newly created activity status.</returns>
        public async Task<Guid> Handle(CreateActivityStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating new activity status with status name: {StatusName}", request.Status);

                // Map command to entity
                var activityStatus = _mapper.Map<VibeCRM.Domain.Entities.TypeStatusEntities.ActivityStatus>(request);

                // Set audit fields
                activityStatus.CreatedDate = DateTime.UtcNow;
                activityStatus.ModifiedDate = activityStatus.CreatedDate;

                // Ensure Active is set to true (soft delete pattern)
                activityStatus.Active = true;

                // Save to repository
                var createdActivityStatus = await _activityStatusRepository.AddAsync(activityStatus, cancellationToken);

                _logger.LogInformation("Successfully created activity status with ID: {ActivityStatusId}", createdActivityStatus.Id);

                return createdActivityStatus.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating activity status: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
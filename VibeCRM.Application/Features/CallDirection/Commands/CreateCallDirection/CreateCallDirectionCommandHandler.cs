using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.CallDirection.Commands.CreateCallDirection
{
    /// <summary>
    /// Handler for the CreateCallDirectionCommand.
    /// Processes requests to create a new call direction.
    /// </summary>
    public class CreateCallDirectionCommandHandler : IRequestHandler<CreateCallDirectionCommand, Guid>
    {
        private readonly ICallDirectionRepository _callDirectionRepository;
        private readonly ILogger<CreateCallDirectionCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCallDirectionCommandHandler"/> class.
        /// </summary>
        /// <param name="callDirectionRepository">The call direction repository for data access.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when callDirectionRepository or logger is null.</exception>
        public CreateCallDirectionCommandHandler(
            ICallDirectionRepository callDirectionRepository,
            ILogger<CreateCallDirectionCommandHandler> logger)
        {
            _callDirectionRepository = callDirectionRepository ?? throw new ArgumentNullException(nameof(callDirectionRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateCallDirectionCommand by creating a new call direction in the database.
        /// </summary>
        /// <param name="request">The command containing the call direction details to create.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>The ID of the newly created call direction.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        public async Task<Guid> Handle(CreateCallDirectionCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Creating new call direction: {Direction}", request.Direction);

            var entity = new Domain.Entities.TypeStatusEntities.CallDirection
            {
                Id = Guid.NewGuid(),
                Direction = request.Direction,
                Description = request.Description,
                OrdinalPosition = request.OrdinalPosition,
                CreatedBy = Guid.Parse(request.CreatedBy),
                CreatedDate = DateTime.UtcNow,
                ModifiedBy = Guid.Parse(request.CreatedBy),
                ModifiedDate = DateTime.UtcNow,
                Active = true
            };

            await _callDirectionRepository.AddAsync(entity, cancellationToken);

            _logger.LogInformation("Successfully created call direction with ID: {Id}", entity.Id);

            return entity.Id;
        }
    }
}
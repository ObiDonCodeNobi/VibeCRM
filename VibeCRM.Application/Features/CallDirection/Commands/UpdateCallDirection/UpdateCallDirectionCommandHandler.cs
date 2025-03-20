using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.CallDirection.Commands.UpdateCallDirection
{
    /// <summary>
    /// Handler for the UpdateCallDirectionCommand.
    /// Processes requests to update an existing call direction.
    /// </summary>
    public class UpdateCallDirectionCommandHandler : IRequestHandler<UpdateCallDirectionCommand, bool>
    {
        private readonly ICallDirectionRepository _callDirectionRepository;
        private readonly ILogger<UpdateCallDirectionCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCallDirectionCommandHandler"/> class.
        /// </summary>
        /// <param name="callDirectionRepository">The call direction repository for data access.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when callDirectionRepository or logger is null.</exception>
        public UpdateCallDirectionCommandHandler(
            ICallDirectionRepository callDirectionRepository,
            ILogger<UpdateCallDirectionCommandHandler> logger)
        {
            _callDirectionRepository = callDirectionRepository ?? throw new ArgumentNullException(nameof(callDirectionRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateCallDirectionCommand by updating an existing call direction in the database.
        /// </summary>
        /// <param name="request">The command containing the call direction details to update.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>True if the update was successful, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        public async Task<bool> Handle(UpdateCallDirectionCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Updating call direction with ID: {Id}", request.Id);

            var existingEntity = await _callDirectionRepository.GetByIdAsync(request.Id, cancellationToken);
            if (existingEntity == null)
            {
                _logger.LogWarning("Call direction with ID: {Id} not found", request.Id);
                return false;
            }

            // Update entity properties
            existingEntity.Direction = request.Direction;
            existingEntity.Description = request.Description;
            existingEntity.OrdinalPosition = request.OrdinalPosition;
            existingEntity.ModifiedBy = Guid.Parse(request.ModifiedBy);
            existingEntity.ModifiedDate = DateTime.UtcNow;

            await _callDirectionRepository.UpdateAsync(existingEntity, cancellationToken);

            _logger.LogInformation("Successfully updated call direction with ID: {Id}", request.Id);

            return true;
        }
    }
}
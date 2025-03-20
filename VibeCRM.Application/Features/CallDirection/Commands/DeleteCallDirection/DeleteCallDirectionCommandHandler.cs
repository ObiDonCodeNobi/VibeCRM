using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.CallDirection.Commands.DeleteCallDirection
{
    /// <summary>
    /// Handler for the DeleteCallDirectionCommand.
    /// Processes requests to soft delete an existing call direction by setting its Active property to false.
    /// </summary>
    public class DeleteCallDirectionCommandHandler : IRequestHandler<DeleteCallDirectionCommand, bool>
    {
        private readonly ICallDirectionRepository _callDirectionRepository;
        private readonly ILogger<DeleteCallDirectionCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCallDirectionCommandHandler"/> class.
        /// </summary>
        /// <param name="callDirectionRepository">The call direction repository for data access.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when callDirectionRepository or logger is null.</exception>
        public DeleteCallDirectionCommandHandler(
            ICallDirectionRepository callDirectionRepository,
            ILogger<DeleteCallDirectionCommandHandler> logger)
        {
            _callDirectionRepository = callDirectionRepository ?? throw new ArgumentNullException(nameof(callDirectionRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteCallDirectionCommand by soft deleting an existing call direction in the database.
        /// </summary>
        /// <param name="request">The command containing the ID of the call direction to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>True if the deletion was successful, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        public async Task<bool> Handle(DeleteCallDirectionCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Soft deleting call direction with ID: {Id}", request.Id);

            var existingEntity = await _callDirectionRepository.GetByIdAsync(request.Id, cancellationToken);
            if (existingEntity == null)
            {
                _logger.LogWarning("Call direction with ID: {Id} not found", request.Id);
                return false;
            }

            // Update the entity to mark it as inactive (soft delete)
            existingEntity.Active = false;
            existingEntity.ModifiedBy = Guid.Parse(request.ModifiedBy);
            existingEntity.ModifiedDate = DateTime.UtcNow;

            await _callDirectionRepository.UpdateAsync(existingEntity, cancellationToken);

            _logger.LogInformation("Successfully soft deleted call direction with ID: {Id}", request.Id);

            return true;
        }
    }
}
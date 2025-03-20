using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Call.Commands.DeleteCall
{
    /// <summary>
    /// Handler for processing DeleteCallCommand requests.
    /// Implements the CQRS command handler pattern for soft-deleting call entities.
    /// </summary>
    public class DeleteCallCommandHandler : IRequestHandler<DeleteCallCommand, bool>
    {
        private readonly ICallRepository _callRepository;
        private readonly ILogger<DeleteCallCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCallCommandHandler"/> class.
        /// </summary>
        /// <param name="callRepository">The call repository for database operations.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public DeleteCallCommandHandler(
            ICallRepository callRepository,
            ILogger<DeleteCallCommandHandler> logger)
        {
            _callRepository = callRepository ?? throw new ArgumentNullException(nameof(callRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteCallCommand by soft-deleting an existing call entity in the database.
        /// </summary>
        /// <param name="request">The DeleteCallCommand containing the call ID to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the call ID is empty.</exception>
        public async Task<bool> Handle(DeleteCallCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Id == Guid.Empty) throw new ArgumentException("Call ID cannot be empty", nameof(request.Id));

            try
            {
                // Get the existing call (Active=1 filter is applied in the repository)
                var existingCall = await _callRepository.GetByIdAsync(request.Id, cancellationToken);

                if (existingCall == null)
                {
                    _logger.LogWarning("Call with ID {CallId} not found or is already inactive", request.Id);
                    return false;
                }

                // Update the modified by information before deletion
                existingCall.ModifiedBy = request.ModifiedBy;
                existingCall.ModifiedDate = DateTime.UtcNow;

                // First update the entity to save the modified by information
                await _callRepository.UpdateAsync(existingCall, cancellationToken);

                // Then soft delete the call by ID (sets Active = 0)
                var result = await _callRepository.DeleteAsync(request.Id, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Soft-deleted call with ID: {CallId}", request.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to soft-delete call with ID: {CallId}", request.Id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting call with ID {CallId}: {ErrorMessage}",
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}
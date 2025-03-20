using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.CallType.Commands.DeleteCallType
{
    /// <summary>
    /// Handler for processing the DeleteCallTypeCommand.
    /// Implements IRequestHandler to handle the command and return a boolean indicating success.
    /// </summary>
    public class DeleteCallTypeCommandHandler : IRequestHandler<DeleteCallTypeCommand, bool>
    {
        private readonly ICallTypeRepository _callTypeRepository;
        private readonly ILogger<DeleteCallTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCallTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="callTypeRepository">The call type repository for data access operations.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters is null.</exception>
        public DeleteCallTypeCommandHandler(
            ICallTypeRepository callTypeRepository,
            ILogger<DeleteCallTypeCommandHandler> logger)
        {
            _callTypeRepository = callTypeRepository ?? throw new ArgumentNullException(nameof(callTypeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteCallTypeCommand by soft deleting an existing call type in the database.
        /// </summary>
        /// <param name="request">The DeleteCallTypeCommand containing the ID of the call type to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the call type could not be deleted.</exception>
        public async Task<bool> Handle(DeleteCallTypeCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Deleting call type with ID: {Id}", request.Id);

            try
            {
                // Get the existing call type
                var existingCallType = await _callTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingCallType == null)
                {
                    throw new InvalidOperationException($"Call type with ID {request.Id} not found.");
                }

                // Delete the entity (soft delete - sets Active = false)
                await _callTypeRepository.DeleteAsync(request.Id, cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting call type with ID: {Id}", request.Id);
                throw new InvalidOperationException($"Failed to delete call type with ID: {request.Id}", ex);
            }
        }
    }
}
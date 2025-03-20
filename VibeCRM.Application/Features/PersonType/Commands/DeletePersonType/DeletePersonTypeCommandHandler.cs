using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PersonType.Commands.DeletePersonType
{
    /// <summary>
    /// Handler for the DeletePersonTypeCommand.
    /// Processes requests to soft-delete person types in the system.
    /// </summary>
    public class DeletePersonTypeCommandHandler : IRequestHandler<DeletePersonTypeCommand, bool>
    {
        private readonly IPersonTypeRepository _personTypeRepository;
        private readonly ILogger<DeletePersonTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the DeletePersonTypeCommandHandler class.
        /// </summary>
        /// <param name="personTypeRepository">The repository for person type operations.</param>
        /// <param name="logger">The logger for diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        public DeletePersonTypeCommandHandler(
            IPersonTypeRepository personTypeRepository,
            ILogger<DeletePersonTypeCommandHandler> logger)
        {
            _personTypeRepository = personTypeRepository ?? throw new ArgumentNullException(nameof(personTypeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeletePersonTypeCommand by soft-deleting a person type in the repository.
        /// </summary>
        /// <param name="request">The command containing the ID of the person type to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>True if the person type was deleted successfully; otherwise, false.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the person type could not be deleted.</exception>
        public async Task<bool> Handle(DeletePersonTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Deleting person type with ID: {PersonTypeId}", request.Id);

                // Check if the person type exists
                var existingPersonType = await _personTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingPersonType == null)
                {
                    _logger.LogWarning("Person type with ID {PersonTypeId} not found", request.Id);
                    return false;
                }

                // Convert ModifiedBy from string to Guid
                Guid modifiedByGuid = string.IsNullOrEmpty(request.ModifiedBy)
                    ? Guid.Empty
                    : Guid.Parse(request.ModifiedBy);

                // Delete the person type (soft delete)
                bool result = await _personTypeRepository.DeleteAsync(request.Id, modifiedByGuid, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Successfully deleted person type with ID: {PersonTypeId}", request.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to delete person type with ID: {PersonTypeId}", request.Id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting person type with ID: {PersonTypeId}. Error: {ErrorMessage}",
                    request.Id, ex.Message);
                throw new InvalidOperationException($"Failed to delete person type: {ex.Message}", ex);
            }
        }
    }
}
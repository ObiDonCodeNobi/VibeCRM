using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Person.Commands.DeletePerson
{
    /// <summary>
    /// Handler for processing DeletePersonCommand requests.
    /// Implements the CQRS command handler pattern for soft-deleting person entities.
    /// </summary>
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, bool>
    {
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<DeletePersonCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeletePersonCommandHandler"/> class.
        /// </summary>
        /// <param name="personRepository">The person repository for database operations.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public DeletePersonCommandHandler(
            IPersonRepository personRepository,
            ILogger<DeletePersonCommandHandler> logger)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeletePersonCommand by soft-deleting an existing person entity in the database.
        /// </summary>
        /// <param name="request">The DeletePersonCommand containing the person ID to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>True if the person was successfully deleted, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                // Retrieve the existing person to update ModifiedBy before deletion
                var existingPerson = await _personRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingPerson == null)
                {
                    _logger.LogWarning("Person with ID {PersonId} not found for deletion", request.Id);
                    return false;
                }

                // Update the ModifiedBy and ModifiedDate before soft deletion
                existingPerson.ModifiedBy = request.ModifiedBy;
                existingPerson.ModifiedDate = DateTime.UtcNow;
                await _personRepository.UpdateAsync(existingPerson, cancellationToken);

                // Perform the soft delete operation
                bool result = await _personRepository.DeleteAsync(request.Id, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Soft-deleted person with ID: {PersonId}", request.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to soft-delete person with ID: {PersonId}", request.Id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting person with ID: {PersonId}", request.Id);
                throw;
            }
        }
    }
}
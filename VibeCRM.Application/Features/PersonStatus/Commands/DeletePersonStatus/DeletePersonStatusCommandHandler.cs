using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PersonStatus.Commands.DeletePersonStatus
{
    /// <summary>
    /// Handler for processing DeletePersonStatusCommand requests.
    /// Implements the CQRS command handler pattern for soft deleting person status entities.
    /// </summary>
    public class DeletePersonStatusCommandHandler : IRequestHandler<DeletePersonStatusCommand, bool>
    {
        private readonly IPersonStatusRepository _personStatusRepository;
        private readonly ILogger<DeletePersonStatusCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeletePersonStatusCommandHandler"/> class.
        /// </summary>
        /// <param name="personStatusRepository">The person status repository for database operations.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public DeletePersonStatusCommandHandler(
            IPersonStatusRepository personStatusRepository,
            ILogger<DeletePersonStatusCommandHandler> logger)
        {
            _personStatusRepository = personStatusRepository ?? throw new ArgumentNullException(nameof(personStatusRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeletePersonStatusCommand by soft deleting a person status entity.
        /// </summary>
        /// <param name="request">The DeletePersonStatusCommand containing the ID of the person status to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>True if the delete was successful; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<bool> Handle(DeletePersonStatusCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Soft deleting person status with ID: {PersonStatusId}", request.Id);

                // Check if the person status exists
                var exists = await _personStatusRepository.ExistsAsync(request.Id, cancellationToken);
                if (!exists)
                {
                    _logger.LogWarning("Person status with ID: {PersonStatusId} not found", request.Id);
                    return false;
                }

                // Convert ModifiedBy from string to Guid
                var modifiedBy = !string.IsNullOrEmpty(request.ModifiedBy) ? Guid.Parse(request.ModifiedBy) : Guid.Empty;

                // Perform soft delete with ModifiedBy
                await _personStatusRepository.DeleteAsync(request.Id, modifiedBy, cancellationToken);

                _logger.LogInformation("Successfully soft deleted person status with ID: {PersonStatusId}", request.Id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while soft deleting person status with ID: {PersonStatusId}. Error: {ErrorMessage}", 
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}

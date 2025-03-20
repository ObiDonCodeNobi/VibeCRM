using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Service.Commands.DeleteService
{
    /// <summary>
    /// Handler for processing DeleteServiceCommand requests.
    /// Implements the CQRS command handler pattern for soft deleting service entities.
    /// </summary>
    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, bool>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly ILogger<DeleteServiceCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteServiceCommandHandler"/> class.
        /// </summary>
        /// <param name="serviceRepository">The service repository for database operations.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public DeleteServiceCommandHandler(
            IServiceRepository serviceRepository,
            ILogger<DeleteServiceCommandHandler> logger)
        {
            _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteServiceCommand by soft deleting a service entity in the database.
        /// This sets the Active property to false rather than removing the record from the database.
        /// </summary>
        /// <param name="request">The DeleteServiceCommand containing the service ID to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>True if the service was successfully deleted, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the service ID is empty.</exception>
        public async Task<bool> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Id == Guid.Empty) throw new ArgumentException("Service ID cannot be empty", nameof(request.Id));

            try
            {
                // Check if the service exists
                var exists = await _serviceRepository.ExistsAsync(request.Id, cancellationToken);
                if (!exists)
                {
                    _logger.LogWarning("Attempted to delete non-existent service with ID: {ServiceId}", request.Id);
                    return false;
                }

                // Soft delete the service by setting Active = 0
                // The BaseRepository.DeleteAsync method handles setting the ModifiedBy and ModifiedDate
                var result = await _serviceRepository.DeleteAsync(request.Id, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Soft deleted service with ID: {ServiceId}", request.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to soft delete service with ID: {ServiceId}", request.Id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error soft deleting service with ID {ServiceId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
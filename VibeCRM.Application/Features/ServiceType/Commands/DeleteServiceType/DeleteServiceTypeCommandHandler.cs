using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ServiceType.Commands.DeleteServiceType
{
    /// <summary>
    /// Handler for the DeleteServiceTypeCommand.
    /// Deletes an existing service type from the database.
    /// </summary>
    public class DeleteServiceTypeCommandHandler : IRequestHandler<DeleteServiceTypeCommand, bool>
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly ILogger<DeleteServiceTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteServiceTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="serviceTypeRepository">The service type repository.</param>
        /// <param name="logger">The logger.</param>
        public DeleteServiceTypeCommandHandler(
            IServiceTypeRepository serviceTypeRepository,
            ILogger<DeleteServiceTypeCommandHandler> logger)
        {
            _serviceTypeRepository = serviceTypeRepository ?? throw new ArgumentNullException(nameof(serviceTypeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteServiceTypeCommand by deleting an existing service type.
        /// </summary>
        /// <param name="request">The command containing the ID of the service type to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the service type was successfully deleted, otherwise false.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the service type with the specified ID is not found.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the deletion process.</exception>
        public async Task<bool> Handle(DeleteServiceTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Deleting service type with ID: {ServiceTypeId}", request.Id);

                // Check if service type exists
                var exists = await _serviceTypeRepository.ExistsAsync(request.Id, cancellationToken);
                if (!exists)
                {
                    _logger.LogError("Service type with ID: {ServiceTypeId} not found", request.Id);
                    throw new KeyNotFoundException($"Service type with ID: {request.Id} not found");
                }

                // Delete from repository (soft delete)
                var result = await _serviceTypeRepository.DeleteAsync(request.Id, cancellationToken);

                _logger.LogInformation("Successfully deleted service type with ID: {ServiceTypeId}", request.Id);

                return result;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting service type: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}

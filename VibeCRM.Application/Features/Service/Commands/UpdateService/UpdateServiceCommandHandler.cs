using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Service.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Service.Commands.UpdateService
{
    /// <summary>
    /// Handler for processing UpdateServiceCommand requests.
    /// Implements the CQRS command handler pattern for updating existing service entities.
    /// </summary>
    public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, ServiceDetailsDto>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateServiceCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateServiceCommandHandler"/> class.
        /// </summary>
        /// <param name="serviceRepository">The service repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public UpdateServiceCommandHandler(
            IServiceRepository serviceRepository,
            IMapper mapper,
            ILogger<UpdateServiceCommandHandler> logger)
        {
            _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateServiceCommand by updating an existing service entity in the database.
        /// </summary>
        /// <param name="request">The UpdateServiceCommand containing the updated service details.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A ServiceDetailsDto representing the updated service.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when service with the same name already exists.</exception>
        /// <exception cref="NotFoundException">Thrown when the service to update is not found.</exception>
        public async Task<ServiceDetailsDto> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            // Get the existing service
            var existingService = await _serviceRepository.GetByIdAsync(request.Id, cancellationToken);
            if (existingService == null)
            {
                _logger.LogWarning("Attempted to update a non-existent service with ID: {ServiceId}", request.Id);
                throw new InvalidOperationException($"Service with ID '{request.Id}' not found.");
            }

            // Check if a service with the same name already exists (excluding the current service)
            var nameExists = await _serviceRepository.ExistsByNameAsync(request.Name, request.Id, cancellationToken);
            if (nameExists)
            {
                _logger.LogWarning("Attempted to update a service with a name that already exists: {ServiceName}", request.Name);
                throw new InvalidOperationException($"A service with the name '{request.Name}' already exists.");
            }

            // Preserve original audit data that shouldn't be modified
            var createdBy = existingService.CreatedBy;
            var createdDate = existingService.CreatedDate;

            // Map update command to entity
            _mapper.Map(request, existingService);

            // Restore preserved values
            existingService.CreatedBy = createdBy;
            existingService.CreatedDate = createdDate;

            try
            {
                // Update the service in the repository
                var updatedService = await _serviceRepository.UpdateAsync(existingService, cancellationToken);
                _logger.LogInformation("Updated service with ID: {ServiceId}", updatedService.Id);

                // Return the detailed DTO of the updated service
                return _mapper.Map<ServiceDetailsDto>(updatedService);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating service with ID {ServiceId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
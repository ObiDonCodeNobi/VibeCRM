using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Service.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Service.Commands.CreateService
{
    /// <summary>
    /// Handler for processing CreateServiceCommand requests.
    /// Implements the CQRS command handler pattern for creating new service entities.
    /// </summary>
    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, ServiceDetailsDto>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateServiceCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateServiceCommandHandler"/> class.
        /// </summary>
        /// <param name="serviceRepository">The service repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public CreateServiceCommandHandler(
            IServiceRepository serviceRepository,
            IMapper mapper,
            ILogger<CreateServiceCommandHandler> logger)
        {
            _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateServiceCommand by creating a new service entity in the database.
        /// </summary>
        /// <param name="request">The CreateServiceCommand containing the service details.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A ServiceDetailsDto representing the newly created service.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when service with the same name already exists.</exception>
        public async Task<ServiceDetailsDto> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            // Check if a service with the same name already exists
            var exists = await _serviceRepository.ExistsByNameAsync(request.Name, null, cancellationToken);
            if (exists)
            {
                _logger.LogWarning("Attempted to create a service with a name that already exists: {ServiceName}", request.Name);
                throw new InvalidOperationException($"A service with the name '{request.Name}' already exists.");
            }

            // Map command to entity and set audit fields
            var service = _mapper.Map<VibeCRM.Domain.Entities.BusinessEntities.Service>(request);

            try
            {
                // Add the service to the repository
                var createdService = await _serviceRepository.AddAsync(service, cancellationToken);
                _logger.LogInformation("Created new service with ID: {ServiceId}", createdService.Id);

                // Return the mapped DTO
                return _mapper.Map<ServiceDetailsDto>(createdService);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating service: {ServiceName}", request.Name);
                throw;
            }
        }
    }
}
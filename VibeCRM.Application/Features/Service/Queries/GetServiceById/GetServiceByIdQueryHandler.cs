using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Service.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Service.Queries.GetServiceById
{
    /// <summary>
    /// Handler for processing GetServiceByIdQuery requests.
    /// Implements the CQRS query handler pattern for retrieving a specific service by ID.
    /// </summary>
    public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, ServiceDetailsDto?>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetServiceByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetServiceByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="serviceRepository">The service repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetServiceByIdQueryHandler(
            IServiceRepository serviceRepository,
            IMapper mapper,
            ILogger<GetServiceByIdQueryHandler> logger)
        {
            _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetServiceByIdQuery by retrieving a specific service from the database.
        /// </summary>
        /// <param name="request">The GetServiceByIdQuery containing the service ID to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A ServiceDetailsDto representing the requested service, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the service ID is empty.</exception>
        public async Task<ServiceDetailsDto?> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Id == Guid.Empty) throw new ArgumentException("Service ID cannot be empty", nameof(request.Id));

            try
            {
                // Get the service by ID with all related entities
                var service = await _serviceRepository.GetByIdWithRelatedEntitiesAsync(request.Id, cancellationToken);

                if (service == null)
                {
                    _logger.LogWarning("Service with ID {ServiceId} not found or is inactive", request.Id);
                    return null;
                }

                _logger.LogInformation("Retrieved service with ID: {ServiceId} and all related entities", request.Id);

                // Map entity to DTO
                return _mapper.Map<ServiceDetailsDto>(service);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving service with ID {ServiceId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
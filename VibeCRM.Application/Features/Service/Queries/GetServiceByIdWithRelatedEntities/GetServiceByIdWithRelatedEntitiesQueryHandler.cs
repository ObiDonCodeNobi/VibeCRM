using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Service.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Service.Queries.GetServiceByIdWithRelatedEntities
{
    /// <summary>
    /// Handler for processing <see cref="GetServiceByIdWithRelatedEntitiesQuery"/> requests
    /// </summary>
    public class GetServiceByIdWithRelatedEntitiesQueryHandler : IRequestHandler<GetServiceByIdWithRelatedEntitiesQuery, ServiceDetailsDto?>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetServiceByIdWithRelatedEntitiesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetServiceByIdWithRelatedEntitiesQueryHandler"/> class
        /// </summary>
        /// <param name="serviceRepository">The service repository for accessing service data</param>
        /// <param name="mapper">The AutoMapper instance for mapping between entities and DTOs</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public GetServiceByIdWithRelatedEntitiesQueryHandler(
            IServiceRepository serviceRepository,
            IMapper mapper,
            ILogger<GetServiceByIdWithRelatedEntitiesQueryHandler> logger)
        {
            _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the <see cref="GetServiceByIdWithRelatedEntitiesQuery"/> request
        /// </summary>
        /// <param name="request">The request to retrieve a service with all related entities</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The service details DTO with all related entities if found, otherwise null</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
        public async Task<ServiceDetailsDto?> Handle(GetServiceByIdWithRelatedEntitiesQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            _logger.LogInformation("Handling GetServiceByIdWithRelatedEntitiesQuery for service ID {ServiceId}", request.Id);

            try
            {
                // Get the service with all related entities
                var service = await _serviceRepository.GetByIdWithRelatedEntitiesAsync(request.Id, cancellationToken);
                if (service == null)
                {
                    _logger.LogWarning("Service with ID {ServiceId} not found", request.Id);
                    return null;
                }

                // Map the service entity to the DTO
                var serviceDetailsDto = _mapper.Map<ServiceDetailsDto>(service);

                _logger.LogInformation("Successfully retrieved service with ID {ServiceId} and all related entities", request.Id);
                return serviceDetailsDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving service with ID {ServiceId} and related entities", request.Id);
                throw;
            }
        }
    }
}
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Service.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Service.Queries.GetAllServicesWithRelatedEntities
{
    /// <summary>
    /// Handler for processing GetAllServicesWithRelatedEntitiesQuery requests
    /// Implements the CQRS query handler pattern for retrieving all services with their related entities
    /// </summary>
    public class GetAllServicesWithRelatedEntitiesQueryHandler : IRequestHandler<GetAllServicesWithRelatedEntitiesQuery, List<ServiceDetailsDto>>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllServicesWithRelatedEntitiesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllServicesWithRelatedEntitiesQueryHandler"/> class
        /// </summary>
        /// <param name="serviceRepository">The service repository for database operations</param>
        /// <param name="mapper">The AutoMapper instance for object mapping</param>
        /// <param name="logger">The logger for capturing diagnostic information</param>
        public GetAllServicesWithRelatedEntitiesQueryHandler(
            IServiceRepository serviceRepository,
            IMapper mapper,
            ILogger<GetAllServicesWithRelatedEntitiesQueryHandler> logger)
        {
            _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllServicesWithRelatedEntitiesQuery by retrieving all active services with their related entities from the database
        /// </summary>
        /// <param name="request">The GetAllServicesWithRelatedEntitiesQuery request object</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A list of ServiceDetailsDto objects representing all active services with their related entities</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null</exception>
        public async Task<List<ServiceDetailsDto>> Handle(GetAllServicesWithRelatedEntitiesQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                // Get all active services with related entities
                var services = await _serviceRepository.GetAllWithRelatedEntitiesAsync(cancellationToken);
                _logger.LogInformation("Retrieved {Count} services with related entities", services.Count());

                // Map entities to DTOs
                return _mapper.Map<List<ServiceDetailsDto>>(services);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all services with related entities: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
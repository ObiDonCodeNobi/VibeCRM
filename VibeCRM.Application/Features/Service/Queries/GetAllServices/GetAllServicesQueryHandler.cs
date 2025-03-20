using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Service.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Service.Queries.GetAllServices
{
    /// <summary>
    /// Handler for processing GetAllServicesQuery requests.
    /// Implements the CQRS query handler pattern for retrieving all services.
    /// </summary>
    public class GetAllServicesQueryHandler : IRequestHandler<GetAllServicesQuery, List<ServiceListDto>>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllServicesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllServicesQueryHandler"/> class.
        /// </summary>
        /// <param name="serviceRepository">The service repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetAllServicesQueryHandler(
            IServiceRepository serviceRepository,
            IMapper mapper,
            ILogger<GetAllServicesQueryHandler> logger)
        {
            _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllServicesQuery by retrieving all active services from the database.
        /// </summary>
        /// <param name="request">The GetAllServicesQuery request object.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A list of ServiceListDto objects representing all active services.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<List<ServiceListDto>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                // Get all active services ordered by name
                var services = await _serviceRepository.GetActiveOrderedAsync(cancellationToken);
                _logger.LogInformation("Retrieved {Count} services", services.Count());

                // Map entities to DTOs
                return _mapper.Map<List<ServiceListDto>>(services);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all services: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
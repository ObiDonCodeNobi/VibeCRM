using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.ServiceType;

namespace VibeCRM.Application.Features.ServiceType.Queries.GetServiceTypeByType
{
    /// <summary>
    /// Handler for the GetServiceTypeByTypeQuery.
    /// Retrieves service types by their type name from the database.
    /// </summary>
    public class GetServiceTypeByTypeQueryHandler : IRequestHandler<GetServiceTypeByTypeQuery, IEnumerable<ServiceTypeListDto>>
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetServiceTypeByTypeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetServiceTypeByTypeQueryHandler"/> class.
        /// </summary>
        /// <param name="serviceTypeRepository">The service type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetServiceTypeByTypeQueryHandler(
            IServiceTypeRepository serviceTypeRepository,
            IMapper mapper,
            ILogger<GetServiceTypeByTypeQueryHandler> logger)
        {
            _serviceTypeRepository = serviceTypeRepository ?? throw new ArgumentNullException(nameof(serviceTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetServiceTypeByTypeQuery by retrieving service types by their type name.
        /// </summary>
        /// <param name="request">The query containing the type name to search for.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of service type list DTOs matching the type name.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<IEnumerable<ServiceTypeListDto>> Handle(GetServiceTypeByTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving service types with type name containing: {TypeName}", request.Type);

                // Get service types by type name
                var serviceTypes = await _serviceTypeRepository.GetByTypeAsync(request.Type, cancellationToken);

                // Map to DTOs
                var serviceTypeDtos = _mapper.Map<IEnumerable<ServiceTypeListDto>>(serviceTypes);

                _logger.LogInformation("Successfully retrieved service types with type name containing: {TypeName}", request.Type);

                return serviceTypeDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving service types by type name: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
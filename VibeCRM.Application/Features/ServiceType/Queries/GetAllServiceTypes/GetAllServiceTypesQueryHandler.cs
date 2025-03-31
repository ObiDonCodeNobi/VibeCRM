using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.ServiceType;

namespace VibeCRM.Application.Features.ServiceType.Queries.GetAllServiceTypes
{
    /// <summary>
    /// Handler for the GetAllServiceTypesQuery.
    /// Retrieves all service types from the database.
    /// </summary>
    public class GetAllServiceTypesQueryHandler : IRequestHandler<GetAllServiceTypesQuery, IEnumerable<ServiceTypeListDto>>
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllServiceTypesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllServiceTypesQueryHandler"/> class.
        /// </summary>
        /// <param name="serviceTypeRepository">The service type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetAllServiceTypesQueryHandler(
            IServiceTypeRepository serviceTypeRepository,
            IMapper mapper,
            ILogger<GetAllServiceTypesQueryHandler> logger)
        {
            _serviceTypeRepository = serviceTypeRepository ?? throw new ArgumentNullException(nameof(serviceTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllServiceTypesQuery by retrieving all service types.
        /// </summary>
        /// <param name="request">The query for retrieving all service types.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of service type list DTOs.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<IEnumerable<ServiceTypeListDto>> Handle(GetAllServiceTypesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving all service types");

                // Get all service types ordered by ordinal position
                var serviceTypes = await _serviceTypeRepository.GetByOrdinalPositionAsync(cancellationToken);

                // Map to DTOs
                var serviceTypeDtos = _mapper.Map<IEnumerable<ServiceTypeListDto>>(serviceTypes);

                _logger.LogInformation("Successfully retrieved all service types");

                return serviceTypeDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all service types: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
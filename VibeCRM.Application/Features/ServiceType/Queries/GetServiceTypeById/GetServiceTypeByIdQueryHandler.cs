using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.ServiceType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ServiceType.Queries.GetServiceTypeById
{
    /// <summary>
    /// Handler for the GetServiceTypeByIdQuery.
    /// Retrieves a service type by its ID from the database.
    /// </summary>
    public class GetServiceTypeByIdQueryHandler : IRequestHandler<GetServiceTypeByIdQuery, ServiceTypeDetailsDto>
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetServiceTypeByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetServiceTypeByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="serviceTypeRepository">The service type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetServiceTypeByIdQueryHandler(
            IServiceTypeRepository serviceTypeRepository,
            IMapper mapper,
            ILogger<GetServiceTypeByIdQueryHandler> logger)
        {
            _serviceTypeRepository = serviceTypeRepository ?? throw new ArgumentNullException(nameof(serviceTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetServiceTypeByIdQuery by retrieving a service type by its ID.
        /// </summary>
        /// <param name="request">The query containing the ID of the service type to retrieve.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The service type details DTO if found, otherwise null.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the service type with the specified ID is not found.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<ServiceTypeDetailsDto> Handle(GetServiceTypeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving service type with ID: {ServiceTypeId}", request.Id);

                // Get service type by ID
                var serviceType = await _serviceTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (serviceType == null)
                {
                    _logger.LogError("Service type with ID: {ServiceTypeId} not found", request.Id);
                    throw new KeyNotFoundException($"Service type with ID: {request.Id} not found");
                }

                // Map to DTO
                var serviceTypeDto = _mapper.Map<ServiceTypeDetailsDto>(serviceType);

                _logger.LogInformation("Successfully retrieved service type with ID: {ServiceTypeId}", serviceTypeDto.Id);

                return serviceTypeDto;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving service type by ID: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
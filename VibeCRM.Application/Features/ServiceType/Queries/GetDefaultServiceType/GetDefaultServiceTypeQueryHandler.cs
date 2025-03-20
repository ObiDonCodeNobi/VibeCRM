using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.ServiceType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ServiceType.Queries.GetDefaultServiceType
{
    /// <summary>
    /// Handler for the GetDefaultServiceTypeQuery.
    /// Retrieves the default service type (the one with the lowest ordinal position) from the database.
    /// </summary>
    public class GetDefaultServiceTypeQueryHandler : IRequestHandler<GetDefaultServiceTypeQuery, ServiceTypeDto>
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDefaultServiceTypeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultServiceTypeQueryHandler"/> class.
        /// </summary>
        /// <param name="serviceTypeRepository">The service type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetDefaultServiceTypeQueryHandler(
            IServiceTypeRepository serviceTypeRepository,
            IMapper mapper,
            ILogger<GetDefaultServiceTypeQueryHandler> logger)
        {
            _serviceTypeRepository = serviceTypeRepository ?? throw new ArgumentNullException(nameof(serviceTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetDefaultServiceTypeQuery by retrieving the default service type from the database.
        /// </summary>
        /// <param name="request">The query to retrieve the default service type.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>The default service type DTO if found, otherwise null.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        public async Task<ServiceTypeDto> Handle(GetDefaultServiceTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving default service type");

                // Get default service type
                var serviceType = await _serviceTypeRepository.GetDefaultAsync(cancellationToken);
                if (serviceType == null)
                {
                    _logger.LogWarning("No default service type found");
                    return new ServiceTypeDto();
                }

                // Map to DTO
                var serviceTypeDto = _mapper.Map<ServiceTypeDto>(serviceType);

                _logger.LogInformation("Successfully retrieved default service type with ID: {ServiceTypeId}", serviceTypeDto.Id);

                return serviceTypeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving default service type: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
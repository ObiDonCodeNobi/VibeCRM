using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Common.Exceptions;
using VibeCRM.Application.Features.AddressType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.AddressType.Queries.GetDefaultAddressType
{
    /// <summary>
    /// Handler for the GetDefaultAddressTypeQuery.
    /// Retrieves the default address type.
    /// </summary>
    public class GetDefaultAddressTypeQueryHandler : IRequestHandler<GetDefaultAddressTypeQuery, AddressTypeDto>
    {
        private readonly IAddressTypeRepository _addressTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDefaultAddressTypeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultAddressTypeQueryHandler"/> class.
        /// </summary>
        /// <param name="addressTypeRepository">The address type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetDefaultAddressTypeQueryHandler(
            IAddressTypeRepository addressTypeRepository,
            IMapper mapper,
            ILogger<GetDefaultAddressTypeQueryHandler> logger)
        {
            _addressTypeRepository = addressTypeRepository ?? throw new ArgumentNullException(nameof(addressTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetDefaultAddressTypeQuery by retrieving the default address type.
        /// </summary>
        /// <param name="request">The query to retrieve the default address type.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The default address type DTO if found; otherwise, null.</returns>
        public async Task<AddressTypeDto> Handle(GetDefaultAddressTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving default address type");

                // Get default address type from repository
                var defaultAddressType = await _addressTypeRepository.GetDefaultAsync(cancellationToken);
                if (defaultAddressType == null)
                {
                    _logger.LogWarning("Default address type not found");
                    throw new NotFoundException(nameof(Domain.Entities.TypeStatusEntities.AddressType), "default");
                }

                // Map to DTO
                var addressTypeDto = _mapper.Map<AddressTypeDto>(defaultAddressType);

                _logger.LogInformation("Successfully retrieved default address type with ID: {AddressTypeId}", addressTypeDto.Id);

                return addressTypeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving default address type: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
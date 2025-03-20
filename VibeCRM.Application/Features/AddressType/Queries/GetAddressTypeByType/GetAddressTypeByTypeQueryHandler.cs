using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.AddressType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.AddressType.Queries.GetAddressTypeByType
{
    /// <summary>
    /// Handler for the GetAddressTypeByTypeQuery.
    /// Retrieves an address type by its type name.
    /// </summary>
    public class GetAddressTypeByTypeQueryHandler : IRequestHandler<GetAddressTypeByTypeQuery, AddressTypeDto?>
    {
        private readonly IAddressTypeRepository _addressTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAddressTypeByTypeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAddressTypeByTypeQueryHandler"/> class.
        /// </summary>
        /// <param name="addressTypeRepository">The address type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetAddressTypeByTypeQueryHandler(
            IAddressTypeRepository addressTypeRepository,
            IMapper mapper,
            ILogger<GetAddressTypeByTypeQueryHandler> logger)
        {
            _addressTypeRepository = addressTypeRepository ?? throw new ArgumentNullException(nameof(addressTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAddressTypeByTypeQuery by retrieving an address type by its type name.
        /// </summary>
        /// <param name="request">The query containing the type name of the address type to retrieve.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The address type DTO if found; otherwise, null.</returns>
        public async Task<AddressTypeDto?> Handle(GetAddressTypeByTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving address type with type name: {AddressTypeName}", request.Type);

                var addressTypes = await _addressTypeRepository.GetByTypeAsync(request.Type, cancellationToken);
                var addressType = addressTypes.FirstOrDefault();

                if (addressType == null)
                {
                    _logger.LogWarning("Address type with type name {AddressTypeName} not found", request.Type);
                    return null;
                }

                var addressTypeDto = _mapper.Map<AddressTypeDto>(addressType);

                _logger.LogInformation("Successfully retrieved address type with type name: {AddressTypeName}", request.Type);

                return addressTypeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving address type with type name {AddressTypeName}: {ErrorMessage}", request.Type, ex.Message);
                throw;
            }
        }
    }
}
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.AddressType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.AddressType.Queries.GetAddressTypeByOrdinalPosition
{
    /// <summary>
    /// Handler for the GetAddressTypeByOrdinalPositionQuery.
    /// Retrieves an address type by its ordinal position.
    /// </summary>
    public class GetAddressTypeByOrdinalPositionQueryHandler : IRequestHandler<GetAddressTypeByOrdinalPositionQuery, AddressTypeDto?>
    {
        private readonly IAddressTypeRepository _addressTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAddressTypeByOrdinalPositionQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAddressTypeByOrdinalPositionQueryHandler"/> class.
        /// </summary>
        /// <param name="addressTypeRepository">The address type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetAddressTypeByOrdinalPositionQueryHandler(
            IAddressTypeRepository addressTypeRepository,
            IMapper mapper,
            ILogger<GetAddressTypeByOrdinalPositionQueryHandler> logger)
        {
            _addressTypeRepository = addressTypeRepository ?? throw new ArgumentNullException(nameof(addressTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAddressTypeByOrdinalPositionQuery by retrieving an address type by its ordinal position.
        /// </summary>
        /// <param name="request">The query containing the ordinal position of the address type to retrieve.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The address type DTO if found; otherwise, null.</returns>
        public async Task<AddressTypeDto?> Handle(GetAddressTypeByOrdinalPositionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving address type with ordinal position: {OrdinalPosition}", request.OrdinalPosition);

                var addressTypes = await _addressTypeRepository.GetByOrdinalPositionAsync(cancellationToken);

                var addressType = addressTypes.FirstOrDefault(at => at.OrdinalPosition == request.OrdinalPosition);

                if (addressType == null)
                {
                    _logger.LogWarning("Address type with ordinal position {OrdinalPosition} not found", request.OrdinalPosition);
                    return null;
                }

                var addressTypeDto = _mapper.Map<AddressTypeDto>(addressType);

                _logger.LogInformation("Successfully retrieved address type with ordinal position: {OrdinalPosition}", request.OrdinalPosition);

                return addressTypeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving address type with ordinal position {OrdinalPosition}: {ErrorMessage}", request.OrdinalPosition, ex.Message);
                throw;
            }
        }
    }
}
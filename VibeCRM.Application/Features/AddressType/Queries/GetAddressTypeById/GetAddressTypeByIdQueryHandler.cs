using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.AddressType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.AddressType.Queries.GetAddressTypeById
{
    /// <summary>
    /// Handler for the GetAddressTypeByIdQuery.
    /// Retrieves an address type by its unique identifier.
    /// </summary>
    public class GetAddressTypeByIdQueryHandler : IRequestHandler<GetAddressTypeByIdQuery, AddressTypeDetailsDto?>
    {
        private readonly IAddressTypeRepository _addressTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAddressTypeByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAddressTypeByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="addressTypeRepository">The address type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetAddressTypeByIdQueryHandler(
            IAddressTypeRepository addressTypeRepository,
            IMapper mapper,
            ILogger<GetAddressTypeByIdQueryHandler> logger)
        {
            _addressTypeRepository = addressTypeRepository ?? throw new ArgumentNullException(nameof(addressTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAddressTypeByIdQuery by retrieving an address type by its ID.
        /// </summary>
        /// <param name="request">The query containing the ID of the address type to retrieve.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The address type details DTO if found; otherwise, null.</returns>
        public async Task<AddressTypeDetailsDto?> Handle(GetAddressTypeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving address type with ID: {AddressTypeId}", request.Id);

                var addressType = await _addressTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (addressType == null)
                {
                    _logger.LogWarning("Address type with ID {AddressTypeId} not found", request.Id);
                    return null;
                }

                var addressTypeDto = _mapper.Map<AddressTypeDetailsDto>(addressType);
                addressTypeDto.AddressCount = 0;

                _logger.LogInformation("Successfully retrieved address type with ID: {AddressTypeId}", request.Id);

                return addressTypeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving address type with ID {AddressTypeId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
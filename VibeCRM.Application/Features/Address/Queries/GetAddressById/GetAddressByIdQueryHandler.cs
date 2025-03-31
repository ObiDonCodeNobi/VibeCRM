using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Address;

namespace VibeCRM.Application.Features.Address.Queries.GetAddressById
{
    /// <summary>
    /// Handler for processing GetAddressByIdQuery requests.
    /// Implements the CQRS query handler pattern for retrieving a specific address by ID.
    /// </summary>
    public class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQuery, AddressDetailsDto?>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAddressByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAddressByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="addressRepository">The address repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetAddressByIdQueryHandler(
            IAddressRepository addressRepository,
            IMapper mapper,
            ILogger<GetAddressByIdQueryHandler> logger)
        {
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAddressByIdQuery by retrieving a specific address from the database.
        /// </summary>
        /// <param name="request">The GetAddressByIdQuery containing the address ID to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>An AddressDetailsDto representing the requested address, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the address ID is empty.</exception>
        public async Task<AddressDetailsDto?> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Id == Guid.Empty) throw new ArgumentException("Address ID cannot be empty", nameof(request.Id));

            try
            {
                // Get the address by ID (Active=1 filter is applied in the repository)
                var address = await _addressRepository.GetByIdAsync(request.Id, cancellationToken);

                if (address == null)
                {
                    _logger.LogWarning("Address with ID {AddressId} not found or is inactive", request.Id);
                    return null;
                }

                _logger.LogInformation("Retrieved address with ID: {AddressId}", request.Id);

                // Map entity to DTO
                return _mapper.Map<AddressDetailsDto>(address);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving address with ID {AddressId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
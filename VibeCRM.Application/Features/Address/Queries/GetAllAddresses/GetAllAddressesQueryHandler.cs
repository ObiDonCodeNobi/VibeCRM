using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Address;

namespace VibeCRM.Application.Features.Address.Queries.GetAllAddresses
{
    /// <summary>
    /// Handler for processing GetAllAddressesQuery requests.
    /// Implements the CQRS query handler pattern for retrieving all addresses.
    /// </summary>
    public class GetAllAddressesQueryHandler : IRequestHandler<GetAllAddressesQuery, List<AddressListDto>>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllAddressesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllAddressesQueryHandler"/> class.
        /// </summary>
        /// <param name="addressRepository">The address repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetAllAddressesQueryHandler(
            IAddressRepository addressRepository,
            IMapper mapper,
            ILogger<GetAllAddressesQueryHandler> logger)
        {
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllAddressesQuery by retrieving all active addresses from the database.
        /// </summary>
        /// <param name="request">The GetAllAddressesQuery request object.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A list of AddressListDto objects representing all active addresses.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<List<AddressListDto>> Handle(GetAllAddressesQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                // Get all active addresses
                var addresses = await _addressRepository.GetAllAsync(cancellationToken);

                // Filter to only include active addresses
                var activeAddresses = addresses.Where(a => a.Active);

                _logger.LogInformation("Retrieved {Count} active addresses", activeAddresses.Count());

                // Map entities to DTOs
                return _mapper.Map<List<AddressListDto>>(activeAddresses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all addresses: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
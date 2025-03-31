using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Address;

namespace VibeCRM.Application.Features.Address.Commands.CreateAddress
{
    /// <summary>
    /// Handler for processing CreateAddressCommand requests.
    /// Implements the CQRS command handler pattern for creating new address entities.
    /// </summary>
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, AddressDetailsDto>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateAddressCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAddressCommandHandler"/> class.
        /// </summary>
        /// <param name="addressRepository">The address repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public CreateAddressCommandHandler(
            IAddressRepository addressRepository,
            IMapper mapper,
            ILogger<CreateAddressCommandHandler> logger)
        {
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateAddressCommand by creating a new address entity in the database.
        /// </summary>
        /// <param name="request">The CreateAddressCommand containing the address details.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A AddressDetailsDto representing the newly created address.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<AddressDetailsDto> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            // Map command to entity
            var address = _mapper.Map<VibeCRM.Domain.Entities.BusinessEntities.Address>(request);

            try
            {
                // Add the address to the repository
                var createdAddress = await _addressRepository.AddAsync(address, cancellationToken);
                _logger.LogInformation("Created new address with ID: {AddressId}", createdAddress.AddressId);

                // Return the mapped DTO
                return _mapper.Map<AddressDetailsDto>(createdAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating address: {Line1}, {City}", request.Line1, request.City);
                throw;
            }
        }
    }
}
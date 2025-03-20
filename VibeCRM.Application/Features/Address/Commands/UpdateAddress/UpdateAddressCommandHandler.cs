using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Address.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Address.Commands.UpdateAddress
{
    /// <summary>
    /// Handler for processing UpdateAddressCommand requests.
    /// Implements the CQRS command handler pattern for updating existing address entities.
    /// </summary>
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, AddressDetailsDto>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAddressCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAddressCommandHandler"/> class.
        /// </summary>
        /// <param name="addressRepository">The address repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public UpdateAddressCommandHandler(
            IAddressRepository addressRepository,
            IMapper mapper,
            ILogger<UpdateAddressCommandHandler> logger)
        {
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateAddressCommand by updating an existing address entity in the database.
        /// </summary>
        /// <param name="request">The UpdateAddressCommand containing the updated address details.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A AddressDetailsDto representing the updated address.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the address ID is empty.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the address to update is not found.</exception>
        public async Task<AddressDetailsDto> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.AddressId == Guid.Empty) throw new ArgumentException("Address ID cannot be empty", nameof(request.AddressId));

            try
            {
                // Get the existing address
                var existingAddress = await _addressRepository.GetByIdAsync(request.AddressId, cancellationToken);
                if (existingAddress == null)
                {
                    _logger.LogWarning("Address with ID {AddressId} not found or is inactive", request.AddressId);
                    throw new InvalidOperationException($"Address with ID {request.AddressId} not found or is inactive");
                }

                // Map updated values to the existing entity
                _mapper.Map(request, existingAddress);

                // Update the address in the repository
                var updatedAddress = await _addressRepository.UpdateAsync(existingAddress, cancellationToken);
                _logger.LogInformation("Updated address with ID: {AddressId}", updatedAddress.AddressId);

                // Return the mapped DTO
                return _mapper.Map<AddressDetailsDto>(updatedAddress);
            }
            catch (Exception ex) when (!(ex is ArgumentNullException || ex is ArgumentException || ex is InvalidOperationException))
            {
                _logger.LogError(ex, "Error occurred while updating address with ID: {AddressId}", request.AddressId);
                throw;
            }
        }
    }
}
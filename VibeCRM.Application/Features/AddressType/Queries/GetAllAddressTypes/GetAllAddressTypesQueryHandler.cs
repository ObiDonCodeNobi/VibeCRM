using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.AddressType;

namespace VibeCRM.Application.Features.AddressType.Queries.GetAllAddressTypes
{
    /// <summary>
    /// Handler for the GetAllAddressTypesQuery.
    /// Retrieves all address types.
    /// </summary>
    public class GetAllAddressTypesQueryHandler : IRequestHandler<GetAllAddressTypesQuery, IEnumerable<AddressTypeListDto>>
    {
        private readonly IAddressTypeRepository _addressTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllAddressTypesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllAddressTypesQueryHandler"/> class.
        /// </summary>
        /// <param name="addressTypeRepository">The address type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetAllAddressTypesQueryHandler(
            IAddressTypeRepository addressTypeRepository,
            IMapper mapper,
            ILogger<GetAllAddressTypesQueryHandler> logger)
        {
            _addressTypeRepository = addressTypeRepository ?? throw new ArgumentNullException(nameof(addressTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllAddressTypesQuery by retrieving all address types.
        /// </summary>
        /// <param name="request">The query to retrieve all address types.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of address type list DTOs.</returns>
        public async Task<IEnumerable<AddressTypeListDto>> Handle(GetAllAddressTypesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving all address types");

                // Get all address types from repository
                var addressTypes = await _addressTypeRepository.GetAllAsync(cancellationToken);

                // Map to DTOs
                var addressTypeDtos = _mapper.Map<IEnumerable<AddressTypeListDto>>(addressTypes);

                // For each DTO, we would typically set the AddressCount property
                // This would be implemented in the repository or a separate service
                // For now, we'll set it to 0 as a placeholder

                _logger.LogInformation("Successfully retrieved all address types");

                return addressTypeDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all address types: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
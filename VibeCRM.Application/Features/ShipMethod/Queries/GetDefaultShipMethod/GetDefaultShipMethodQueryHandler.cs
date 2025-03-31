using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.ShipMethod;

namespace VibeCRM.Application.Features.ShipMethod.Queries.GetDefaultShipMethod
{
    /// <summary>
    /// Handler for the GetDefaultShipMethodQuery.
    /// Retrieves the default shipping method (the one with the lowest ordinal position) from the database.
    /// </summary>
    public class GetDefaultShipMethodQueryHandler : IRequestHandler<GetDefaultShipMethodQuery, ShipMethodDto>
    {
        private readonly IShipMethodRepository _shipMethodRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDefaultShipMethodQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultShipMethodQueryHandler"/> class.
        /// </summary>
        /// <param name="shipMethodRepository">The ship method repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetDefaultShipMethodQueryHandler(
            IShipMethodRepository shipMethodRepository,
            IMapper mapper,
            ILogger<GetDefaultShipMethodQueryHandler> logger)
        {
            _shipMethodRepository = shipMethodRepository ?? throw new ArgumentNullException(nameof(shipMethodRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetDefaultShipMethodQuery by retrieving the default ship method from the database.
        /// </summary>
        /// <param name="request">The query to retrieve the default ship method.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>The default ship method DTO if found, otherwise null.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        public async Task<ShipMethodDto> Handle(GetDefaultShipMethodQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving default shipping method");

                // Get default shipping method
                var shipMethod = await _shipMethodRepository.GetDefaultAsync(cancellationToken);
                if (shipMethod == null)
                {
                    _logger.LogWarning("No default shipping method found");
                    return new ShipMethodDto();
                }

                // Map to DTO
                var shipMethodDto = _mapper.Map<ShipMethodDto>(shipMethod);

                _logger.LogInformation("Successfully retrieved default shipping method with ID: {ShipMethodId}", shipMethodDto.Id);

                return shipMethodDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving default shipping method: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
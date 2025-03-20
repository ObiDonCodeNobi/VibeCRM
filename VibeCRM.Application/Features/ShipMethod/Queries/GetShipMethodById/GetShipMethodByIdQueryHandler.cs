using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.ShipMethod.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ShipMethod.Queries.GetShipMethodById
{
    /// <summary>
    /// Handler for the GetShipMethodByIdQuery.
    /// Retrieves a shipping method by its unique identifier.
    /// </summary>
    public class GetShipMethodByIdQueryHandler : IRequestHandler<GetShipMethodByIdQuery, ShipMethodDetailsDto>
    {
        private readonly IShipMethodRepository _shipMethodRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetShipMethodByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetShipMethodByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="shipMethodRepository">The ship method repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetShipMethodByIdQueryHandler(
            IShipMethodRepository shipMethodRepository,
            IMapper mapper,
            ILogger<GetShipMethodByIdQueryHandler> logger)
        {
            _shipMethodRepository = shipMethodRepository ?? throw new ArgumentNullException(nameof(shipMethodRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetShipMethodByIdQuery by retrieving a ship method by its ID from the database.
        /// </summary>
        /// <param name="request">The query containing the ID of the ship method to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A ShipMethodDetailsDto representing the requested ship method, or null if not found.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<ShipMethodDetailsDto> Handle(GetShipMethodByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving shipping method with ID: {ShipMethodId}", request.Id);

                // Get shipping method by ID
                var shipMethod = await _shipMethodRepository.GetByIdAsync(request.Id, cancellationToken);
                if (shipMethod == null)
                {
                    _logger.LogWarning("Shipping method with ID: {ShipMethodId} not found", request.Id);
                    return new ShipMethodDetailsDto();
                }

                // Map to DTO
                var shipMethodDto = _mapper.Map<ShipMethodDetailsDto>(shipMethod);

                // Note: In a real application, you might want to populate OrderCount for the shipping method
                // This would typically involve querying a separate repository or using a join in SQL

                _logger.LogInformation("Successfully retrieved shipping method with ID: {ShipMethodId}", shipMethodDto.Id);

                return shipMethodDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving shipping method with ID {ShipMethodId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}

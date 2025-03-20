using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.ShipMethod.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ShipMethod.Queries.GetAllShipMethods
{
    /// <summary>
    /// Handler for the GetAllShipMethodsQuery.
    /// Retrieves all active shipping methods ordered by their ordinal position.
    /// </summary>
    public class GetAllShipMethodsQueryHandler : IRequestHandler<GetAllShipMethodsQuery, IEnumerable<ShipMethodListDto>>
    {
        private readonly IShipMethodRepository _shipMethodRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllShipMethodsQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllShipMethodsQueryHandler"/> class.
        /// </summary>
        /// <param name="shipMethodRepository">The ship method repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetAllShipMethodsQueryHandler(
            IShipMethodRepository shipMethodRepository,
            IMapper mapper,
            ILogger<GetAllShipMethodsQueryHandler> logger)
        {
            _shipMethodRepository = shipMethodRepository ?? throw new ArgumentNullException(nameof(shipMethodRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllShipMethodsQuery by retrieving all active shipping methods.
        /// </summary>
        /// <param name="request">The query for retrieving all shipping methods.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of shipping method DTOs ordered by their ordinal position.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<IEnumerable<ShipMethodListDto>> Handle(GetAllShipMethodsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving all shipping methods");

                // Get all shipping methods ordered by ordinal position
                var shipMethods = await _shipMethodRepository.GetByOrdinalPositionAsync(cancellationToken);

                // Map to DTOs
                var shipMethodDtos = _mapper.Map<IEnumerable<ShipMethodListDto>>(shipMethods);

                // Note: In a real application, you might want to populate OrderCount for each shipping method
                // This would typically involve querying a separate repository or using a join in SQL

                _logger.LogInformation("Successfully retrieved all shipping methods");

                return shipMethodDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all shipping methods: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}

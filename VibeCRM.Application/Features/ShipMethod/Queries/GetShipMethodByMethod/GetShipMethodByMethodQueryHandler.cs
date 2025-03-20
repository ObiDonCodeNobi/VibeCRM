using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.ShipMethod.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.ShipMethod.Queries.GetShipMethodByMethod
{
    /// <summary>
    /// Handler for the GetShipMethodByMethodQuery.
    /// Retrieves shipping methods by their method name.
    /// </summary>
    public class GetShipMethodByMethodQueryHandler : IRequestHandler<GetShipMethodByMethodQuery, IEnumerable<ShipMethodListDto>>
    {
        private readonly IShipMethodRepository _shipMethodRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetShipMethodByMethodQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetShipMethodByMethodQueryHandler"/> class.
        /// </summary>
        /// <param name="shipMethodRepository">The ship method repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetShipMethodByMethodQueryHandler(
            IShipMethodRepository shipMethodRepository,
            IMapper mapper,
            ILogger<GetShipMethodByMethodQueryHandler> logger)
        {
            _shipMethodRepository = shipMethodRepository ?? throw new ArgumentNullException(nameof(shipMethodRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetShipMethodByMethodQuery by retrieving shipping methods by their method name.
        /// </summary>
        /// <param name="request">The query for retrieving shipping methods by method name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of shipping method DTOs matching the method name.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<IEnumerable<ShipMethodListDto>> Handle(GetShipMethodByMethodQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving shipping methods with method name: {MethodName}", request.Method);

                // Get shipping methods by method name
                var shipMethods = await _shipMethodRepository.GetByMethodAsync(request.Method, cancellationToken);

                // Map to DTOs
                var shipMethodDtos = _mapper.Map<IEnumerable<ShipMethodListDto>>(shipMethods);

                // Note: In a real application, you might want to populate OrderCount for each shipping method
                // This would typically involve querying a separate repository or using a join in SQL

                _logger.LogInformation("Successfully retrieved shipping methods with method name: {MethodName}", request.Method);

                return shipMethodDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving shipping methods with method name {MethodName}: {ErrorMessage}", request.Method, ex.Message);
                throw;
            }
        }
    }
}
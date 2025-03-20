using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.SalesOrder.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.SalesOrder.Queries.GetSalesOrderByNumber
{
    /// <summary>
    /// Handler for processing the GetSalesOrderByNumberQuery
    /// </summary>
    public class GetSalesOrderByNumberQueryHandler : IRequestHandler<GetSalesOrderByNumberQuery, SalesOrderDetailsDto>
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSalesOrderByNumberQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderByNumberQueryHandler"/> class.
        /// </summary>
        /// <param name="salesOrderRepository">The sales order repository for data access operations</param>
        /// <param name="mapper">The AutoMapper instance for object mapping</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public GetSalesOrderByNumberQueryHandler(
            ISalesOrderRepository salesOrderRepository,
            IMapper mapper,
            ILogger<GetSalesOrderByNumberQueryHandler> logger)
        {
            _salesOrderRepository = salesOrderRepository ?? throw new ArgumentNullException(nameof(salesOrderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetSalesOrderByNumberQuery by retrieving a sales order by its number from the database.
        /// </summary>
        /// <param name="request">The query containing the number of the sales order to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A SalesOrderDetailsDto representing the requested sales order, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<SalesOrderDetailsDto> Handle(GetSalesOrderByNumberQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Number))
            {
                throw new ArgumentException("Sales order number cannot be null or empty", nameof(request.Number));
            }

            try
            {
                _logger.LogInformation("Retrieving sales order with number {SalesOrderNumber}", request.Number);

                // Get the sales orders from the repository
                var salesOrders = await _salesOrderRepository.GetByNumberAsync(request.Number, cancellationToken);

                // Get the first active sales order with the specified number
                var salesOrder = salesOrders.FirstOrDefault(so => so.Active);

                if (salesOrder == null)
                {
                    _logger.LogWarning("Sales order with number {SalesOrderNumber} not found", request.Number);
                    return new SalesOrderDetailsDto();
                }

                // Map the entity to DTO
                var salesOrderDto = _mapper.Map<SalesOrderDetailsDto>(salesOrder);

                _logger.LogInformation("Successfully retrieved sales order with number {SalesOrderNumber}", request.Number);

                return salesOrderDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sales order with number {SalesOrderNumber}: {ErrorMessage}", request.Number, ex.Message);
                throw;
            }
        }
    }
}
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.SalesOrder;

namespace VibeCRM.Application.Features.SalesOrder.Queries.GetSalesOrderById
{
    /// <summary>
    /// Handler for processing the GetSalesOrderByIdQuery
    /// </summary>
    public class GetSalesOrderByIdQueryHandler : IRequestHandler<GetSalesOrderByIdQuery, SalesOrderDetailsDto>
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSalesOrderByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="salesOrderRepository">The sales order repository for data access operations</param>
        /// <param name="mapper">The AutoMapper instance for object mapping</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public GetSalesOrderByIdQueryHandler(
            ISalesOrderRepository salesOrderRepository,
            IMapper mapper,
            ILogger<GetSalesOrderByIdQueryHandler> logger)
        {
            _salesOrderRepository = salesOrderRepository ?? throw new ArgumentNullException(nameof(salesOrderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetSalesOrderByIdQuery by retrieving a sales order by its ID from the database.
        /// </summary>
        /// <param name="request">The query containing the ID of the sales order to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A SalesOrderDetailsDto representing the requested sales order, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<SalesOrderDetailsDto> Handle(GetSalesOrderByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            try
            {
                _logger.LogInformation("Retrieving sales order with ID {SalesOrderId}", request.Id);

                // Get the sales order from the repository with all related entities
                var salesOrder = await _salesOrderRepository.GetByIdWithRelatedEntitiesAsync(request.Id, cancellationToken);

                if (salesOrder == null)
                {
                    _logger.LogWarning("Sales order with ID {SalesOrderId} not found", request.Id);
                    return new SalesOrderDetailsDto();
                }

                // Map the entity to DTO
                var salesOrderDto = _mapper.Map<SalesOrderDetailsDto>(salesOrder);

                _logger.LogInformation("Successfully retrieved sales order with ID {SalesOrderId}", request.Id);

                return salesOrderDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sales order with ID {SalesOrderId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
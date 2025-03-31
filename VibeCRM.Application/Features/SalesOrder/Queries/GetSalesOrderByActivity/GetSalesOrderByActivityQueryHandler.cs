using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.SalesOrder;

namespace VibeCRM.Application.Features.SalesOrder.Queries.GetSalesOrderByActivity
{
    /// <summary>
    /// Handler for processing the GetSalesOrderByActivityQuery
    /// </summary>
    public class GetSalesOrderByActivityQueryHandler : IRequestHandler<GetSalesOrderByActivityQuery, IEnumerable<SalesOrderListDto>>
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSalesOrderByActivityQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderByActivityQueryHandler"/> class.
        /// </summary>
        /// <param name="salesOrderRepository">The sales order repository for data access operations</param>
        /// <param name="mapper">The AutoMapper instance for object mapping</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public GetSalesOrderByActivityQueryHandler(
            ISalesOrderRepository salesOrderRepository,
            IMapper mapper,
            ILogger<GetSalesOrderByActivityQueryHandler> logger)
        {
            _salesOrderRepository = salesOrderRepository ?? throw new ArgumentNullException(nameof(salesOrderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetSalesOrderByActivityQuery by retrieving all sales orders associated with a specific activity
        /// </summary>
        /// <param name="request">The query containing the activity ID to filter sales orders by</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales order list DTOs associated with the specified activity</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
        public async Task<IEnumerable<SalesOrderListDto>> Handle(GetSalesOrderByActivityQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            try
            {
                _logger.LogInformation("Retrieving sales orders for activity with ID {ActivityId}", request.ActivityId);

                // Get the sales orders from the repository
                var salesOrders = await _salesOrderRepository.GetByActivityAsync(request.ActivityId, cancellationToken);

                // Map the entities to DTOs
                var salesOrderDtos = _mapper.Map<IEnumerable<SalesOrderListDto>>(salesOrders);

                _logger.LogInformation("Successfully retrieved sales orders for activity with ID {ActivityId}", request.ActivityId);

                return salesOrderDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sales orders for activity with ID {ActivityId}: {ErrorMessage}", request.ActivityId, ex.Message);
                throw;
            }
        }
    }
}
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.SalesOrder;

namespace VibeCRM.Application.Features.SalesOrder.Queries.GetAllSalesOrders
{
    /// <summary>
    /// Handler for processing the GetAllSalesOrdersQuery
    /// </summary>
    public class GetAllSalesOrdersQueryHandler : IRequestHandler<GetAllSalesOrdersQuery, IEnumerable<SalesOrderListDto>>
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllSalesOrdersQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllSalesOrdersQueryHandler"/> class.
        /// </summary>
        /// <param name="salesOrderRepository">The sales order repository for data access operations</param>
        /// <param name="mapper">The AutoMapper instance for object mapping</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public GetAllSalesOrdersQueryHandler(
            ISalesOrderRepository salesOrderRepository,
            IMapper mapper,
            ILogger<GetAllSalesOrdersQueryHandler> logger)
        {
            _salesOrderRepository = salesOrderRepository ?? throw new ArgumentNullException(nameof(salesOrderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllSalesOrdersQuery by retrieving all sales orders from the database
        /// </summary>
        /// <param name="request">The query parameters for retrieving sales orders</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales order list DTOs</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
        public async Task<IEnumerable<SalesOrderListDto>> Handle(GetAllSalesOrdersQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            try
            {
                _logger.LogInformation("Retrieving all sales orders");

                // Get all sales orders from the repository
                var salesOrders = await _salesOrderRepository.GetAllAsync(cancellationToken);

                // Filter inactive sales orders if requested
                if (!request.IncludeInactive)
                {
                    salesOrders = salesOrders.Where(so => so.Active).ToList();
                }

                // Map the entities to DTOs
                var salesOrderDtos = _mapper.Map<IEnumerable<SalesOrderListDto>>(salesOrders);

                _logger.LogInformation("Successfully retrieved all sales orders");

                return salesOrderDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all sales orders: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
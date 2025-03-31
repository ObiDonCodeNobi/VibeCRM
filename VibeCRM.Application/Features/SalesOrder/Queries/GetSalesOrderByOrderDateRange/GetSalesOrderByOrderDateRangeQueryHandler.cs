using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.SalesOrder;

namespace VibeCRM.Application.Features.SalesOrder.Queries.GetSalesOrderByOrderDateRange
{
    /// <summary>
    /// Handler for processing the GetSalesOrderByOrderDateRangeQuery
    /// </summary>
    public class GetSalesOrderByOrderDateRangeQueryHandler : IRequestHandler<GetSalesOrderByOrderDateRangeQuery, IEnumerable<SalesOrderListDto>>
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSalesOrderByOrderDateRangeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderByOrderDateRangeQueryHandler"/> class.
        /// </summary>
        /// <param name="salesOrderRepository">The sales order repository for data access operations</param>
        /// <param name="mapper">The AutoMapper instance for object mapping</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public GetSalesOrderByOrderDateRangeQueryHandler(
            ISalesOrderRepository salesOrderRepository,
            IMapper mapper,
            ILogger<GetSalesOrderByOrderDateRangeQueryHandler> logger)
        {
            _salesOrderRepository = salesOrderRepository ?? throw new ArgumentNullException(nameof(salesOrderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetSalesOrderByOrderDateRangeQuery by retrieving all sales orders within a specific order date range
        /// </summary>
        /// <param name="request">The query containing the start and end dates to filter sales orders by</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales order list DTOs within the specified date range</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
        /// <exception cref="ArgumentException">Thrown when the date range is invalid</exception>
        public async Task<IEnumerable<SalesOrderListDto>> Handle(GetSalesOrderByOrderDateRangeQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.EndDate < request.StartDate)
            {
                throw new ArgumentException("End date cannot be earlier than start date", nameof(request.EndDate));
            }

            try
            {
                _logger.LogInformation("Retrieving sales orders with order date between {StartDate} and {EndDate}",
                    request.StartDate, request.EndDate);

                // Get the sales orders from the repository
                var salesOrders = await _salesOrderRepository.GetByOrderDateRangeAsync(
                    request.StartDate, request.EndDate, cancellationToken);

                // Map the entities to DTOs
                var salesOrderDtos = _mapper.Map<IEnumerable<SalesOrderListDto>>(salesOrders);

                _logger.LogInformation("Successfully retrieved sales orders with order date between {StartDate} and {EndDate}",
                    request.StartDate, request.EndDate);

                return salesOrderDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sales orders with order date between {StartDate} and {EndDate}: {ErrorMessage}",
                    request.StartDate, request.EndDate, ex.Message);
                throw;
            }
        }
    }
}
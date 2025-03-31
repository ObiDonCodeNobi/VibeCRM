using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.SalesOrder;

namespace VibeCRM.Application.Features.SalesOrder.Queries.GetSalesOrderByQuote
{
    /// <summary>
    /// Handler for processing the GetSalesOrderByQuoteQuery
    /// </summary>
    public class GetSalesOrderByQuoteQueryHandler : IRequestHandler<GetSalesOrderByQuoteQuery, IEnumerable<SalesOrderListDto>>
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSalesOrderByQuoteQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderByQuoteQueryHandler"/> class.
        /// </summary>
        /// <param name="salesOrderRepository">The sales order repository for data access operations</param>
        /// <param name="mapper">The AutoMapper instance for object mapping</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public GetSalesOrderByQuoteQueryHandler(
            ISalesOrderRepository salesOrderRepository,
            IMapper mapper,
            ILogger<GetSalesOrderByQuoteQueryHandler> logger)
        {
            _salesOrderRepository = salesOrderRepository ?? throw new ArgumentNullException(nameof(salesOrderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetSalesOrderByQuoteQuery by retrieving all sales orders associated with a specific quote
        /// </summary>
        /// <param name="request">The query containing the quote ID to filter sales orders by</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales order list DTOs associated with the specified quote</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
        public async Task<IEnumerable<SalesOrderListDto>> Handle(GetSalesOrderByQuoteQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            try
            {
                _logger.LogInformation("Retrieving sales orders for quote with ID {QuoteId}", request.QuoteId);

                // Get the sales orders from the repository
                var salesOrders = await _salesOrderRepository.GetByQuoteAsync(request.QuoteId, cancellationToken);

                // Map the entities to DTOs
                var salesOrderDtos = _mapper.Map<IEnumerable<SalesOrderListDto>>(salesOrders);

                _logger.LogInformation("Successfully retrieved sales orders for quote with ID {QuoteId}", request.QuoteId);

                return salesOrderDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sales orders for quote with ID {QuoteId}: {ErrorMessage}", request.QuoteId, ex.Message);
                throw;
            }
        }
    }
}
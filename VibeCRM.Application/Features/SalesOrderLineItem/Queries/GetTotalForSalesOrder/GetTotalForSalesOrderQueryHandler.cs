using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Queries.GetTotalForSalesOrder
{
    /// <summary>
    /// Handler for processing the GetTotalForSalesOrderQuery
    /// </summary>
    public class GetTotalForSalesOrderQueryHandler : IRequestHandler<GetTotalForSalesOrderQuery, decimal>
    {
        private readonly ISalesOrderLineItemRepository _salesOrderLineItemRepository;
        private readonly ILogger<GetTotalForSalesOrderQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTotalForSalesOrderQueryHandler"/> class
        /// </summary>
        /// <param name="salesOrderLineItemRepository">The sales order line item repository</param>
        /// <param name="logger">The logger</param>
        public GetTotalForSalesOrderQueryHandler(
            ISalesOrderLineItemRepository salesOrderLineItemRepository,
            ILogger<GetTotalForSalesOrderQueryHandler> logger)
        {
            _salesOrderLineItemRepository = salesOrderLineItemRepository ?? throw new ArgumentNullException(nameof(salesOrderLineItemRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetTotalForSalesOrderQuery
        /// </summary>
        /// <param name="request">The query to calculate the total amount for a sales order</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The total amount for the specified sales order</returns>
        public async Task<decimal> Handle(GetTotalForSalesOrderQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Calculating total amount for sales order with ID {SalesOrderId}", request.SalesOrderId);

            try
            {
                var total = await _salesOrderLineItemRepository.GetTotalForSalesOrderAsync(request.SalesOrderId, cancellationToken);

                _logger.LogInformation("Total amount for sales order with ID {SalesOrderId} is {Total}", request.SalesOrderId, total);

                return total;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating total amount for sales order with ID {SalesOrderId}", request.SalesOrderId);
                throw;
            }
        }
    }
}
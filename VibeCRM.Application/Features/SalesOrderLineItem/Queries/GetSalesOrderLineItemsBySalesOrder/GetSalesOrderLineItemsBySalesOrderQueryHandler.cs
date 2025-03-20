using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.SalesOrderLineItem.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Queries.GetSalesOrderLineItemsBySalesOrder
{
    /// <summary>
    /// Handler for processing the GetSalesOrderLineItemsBySalesOrderQuery
    /// </summary>
    public class GetSalesOrderLineItemsBySalesOrderQueryHandler : IRequestHandler<GetSalesOrderLineItemsBySalesOrderQuery, IEnumerable<SalesOrderLineItemDetailsDto>>
    {
        private readonly ISalesOrderLineItemRepository _salesOrderLineItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSalesOrderLineItemsBySalesOrderQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderLineItemsBySalesOrderQueryHandler"/> class
        /// </summary>
        /// <param name="salesOrderLineItemRepository">The sales order line item repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetSalesOrderLineItemsBySalesOrderQueryHandler(
            ISalesOrderLineItemRepository salesOrderLineItemRepository,
            IMapper mapper,
            ILogger<GetSalesOrderLineItemsBySalesOrderQueryHandler> logger)
        {
            _salesOrderLineItemRepository = salesOrderLineItemRepository ?? throw new ArgumentNullException(nameof(salesOrderLineItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetSalesOrderLineItemsBySalesOrderQuery
        /// </summary>
        /// <param name="request">The query to retrieve sales order line items by sales order ID</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collection of sales order line items for the specified sales order</returns>
        public async Task<IEnumerable<SalesOrderLineItemDetailsDto>> Handle(GetSalesOrderLineItemsBySalesOrderQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving sales order line items for sales order with ID {SalesOrderId}", request.SalesOrderId);

            try
            {
                var salesOrderLineItems = await _salesOrderLineItemRepository.GetBySalesOrderIdAsync(request.SalesOrderId, cancellationToken);

                // Only return active line items
                var activeLineItems = new List<Domain.Entities.BusinessEntities.SalesOrderLineItem>();
                foreach (var lineItem in salesOrderLineItems)
                {
                    if (lineItem.Active)
                    {
                        activeLineItems.Add(lineItem);
                    }
                }

                _logger.LogInformation("Retrieved {Count} active sales order line items for sales order with ID {SalesOrderId}",
                    activeLineItems.Count, request.SalesOrderId);

                return _mapper.Map<IEnumerable<SalesOrderLineItemDetailsDto>>(activeLineItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sales order line items for sales order with ID {SalesOrderId}", request.SalesOrderId);
                throw;
            }
        }
    }
}
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.SalesOrderLineItem;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Queries.GetSalesOrderLineItemsByProduct
{
    /// <summary>
    /// Handler for processing the GetSalesOrderLineItemsByProductQuery
    /// </summary>
    public class GetSalesOrderLineItemsByProductQueryHandler : IRequestHandler<GetSalesOrderLineItemsByProductQuery, IEnumerable<SalesOrderLineItemDetailsDto>>
    {
        private readonly ISalesOrderLineItemRepository _salesOrderLineItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSalesOrderLineItemsByProductQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderLineItemsByProductQueryHandler"/> class
        /// </summary>
        /// <param name="salesOrderLineItemRepository">The sales order line item repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetSalesOrderLineItemsByProductQueryHandler(
            ISalesOrderLineItemRepository salesOrderLineItemRepository,
            IMapper mapper,
            ILogger<GetSalesOrderLineItemsByProductQueryHandler> logger)
        {
            _salesOrderLineItemRepository = salesOrderLineItemRepository ?? throw new ArgumentNullException(nameof(salesOrderLineItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetSalesOrderLineItemsByProductQuery
        /// </summary>
        /// <param name="request">The query to retrieve sales order line items by product ID</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collection of sales order line items for the specified product</returns>
        public async Task<IEnumerable<SalesOrderLineItemDetailsDto>> Handle(GetSalesOrderLineItemsByProductQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving sales order line items for product with ID {ProductId}", request.ProductId);

            try
            {
                var salesOrderLineItems = await _salesOrderLineItemRepository.GetByProductIdAsync(request.ProductId, cancellationToken);

                // Only return active line items
                var activeLineItems = salesOrderLineItems.Where(item => item.Active).ToList();

                _logger.LogInformation("Retrieved {Count} active sales order line items for product with ID {ProductId}",
                    activeLineItems.Count, request.ProductId);

                return _mapper.Map<IEnumerable<SalesOrderLineItemDetailsDto>>(activeLineItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sales order line items for product with ID {ProductId}", request.ProductId);
                throw;
            }
        }
    }
}
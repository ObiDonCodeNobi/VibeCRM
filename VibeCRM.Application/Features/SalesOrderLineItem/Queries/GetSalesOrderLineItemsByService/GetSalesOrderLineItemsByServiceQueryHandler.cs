using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.SalesOrderLineItem;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Queries.GetSalesOrderLineItemsByService
{
    /// <summary>
    /// Handler for processing the GetSalesOrderLineItemsByServiceQuery
    /// </summary>
    public class GetSalesOrderLineItemsByServiceQueryHandler : IRequestHandler<GetSalesOrderLineItemsByServiceQuery, IEnumerable<SalesOrderLineItemDetailsDto>>
    {
        private readonly ISalesOrderLineItemRepository _salesOrderLineItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSalesOrderLineItemsByServiceQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderLineItemsByServiceQueryHandler"/> class
        /// </summary>
        /// <param name="salesOrderLineItemRepository">The sales order line item repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetSalesOrderLineItemsByServiceQueryHandler(
            ISalesOrderLineItemRepository salesOrderLineItemRepository,
            IMapper mapper,
            ILogger<GetSalesOrderLineItemsByServiceQueryHandler> logger)
        {
            _salesOrderLineItemRepository = salesOrderLineItemRepository ?? throw new ArgumentNullException(nameof(salesOrderLineItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetSalesOrderLineItemsByServiceQuery
        /// </summary>
        /// <param name="request">The query to retrieve sales order line items by service ID</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collection of sales order line items for the specified service</returns>
        public async Task<IEnumerable<SalesOrderLineItemDetailsDto>> Handle(GetSalesOrderLineItemsByServiceQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving sales order line items for service with ID {ServiceId}", request.ServiceId);

            try
            {
                var salesOrderLineItems = await _salesOrderLineItemRepository.GetByServiceIdAsync(request.ServiceId, cancellationToken);

                // Only return active line items
                var activeLineItems = salesOrderLineItems.Where(item => item.Active).ToList();

                _logger.LogInformation("Retrieved {Count} active sales order line items for service with ID {ServiceId}",
                    activeLineItems.Count, request.ServiceId);

                return _mapper.Map<IEnumerable<SalesOrderLineItemDetailsDto>>(activeLineItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sales order line items for service with ID {ServiceId}", request.ServiceId);
                throw;
            }
        }
    }
}
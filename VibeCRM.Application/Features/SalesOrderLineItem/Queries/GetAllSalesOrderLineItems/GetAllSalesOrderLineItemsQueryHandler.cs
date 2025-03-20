using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.SalesOrderLineItem.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Queries.GetAllSalesOrderLineItems
{
    /// <summary>
    /// Handler for processing the GetAllSalesOrderLineItemsQuery
    /// </summary>
    public class GetAllSalesOrderLineItemsQueryHandler : IRequestHandler<GetAllSalesOrderLineItemsQuery, IEnumerable<SalesOrderLineItemListDto>>
    {
        private readonly ISalesOrderLineItemRepository _salesOrderLineItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllSalesOrderLineItemsQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllSalesOrderLineItemsQueryHandler"/> class
        /// </summary>
        /// <param name="salesOrderLineItemRepository">The sales order line item repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetAllSalesOrderLineItemsQueryHandler(
            ISalesOrderLineItemRepository salesOrderLineItemRepository,
            IMapper mapper,
            ILogger<GetAllSalesOrderLineItemsQueryHandler> logger)
        {
            _salesOrderLineItemRepository = salesOrderLineItemRepository ?? throw new ArgumentNullException(nameof(salesOrderLineItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllSalesOrderLineItemsQuery
        /// </summary>
        /// <param name="request">The query to retrieve all sales order line items</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collection of all sales order line items</returns>
        public async Task<IEnumerable<SalesOrderLineItemListDto>> Handle(GetAllSalesOrderLineItemsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving all sales order line items");

            try
            {
                var salesOrderLineItems = await _salesOrderLineItemRepository.GetAllAsync(cancellationToken);

                // Filter inactive items if requested
                if (!request.IncludeInactive)
                {
                    salesOrderLineItems = salesOrderLineItems.Where(item => item.Active).ToList();
                }

                _logger.LogInformation("Retrieved {Count} sales order line items", salesOrderLineItems.Count());

                return _mapper.Map<IEnumerable<SalesOrderLineItemListDto>>(salesOrderLineItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all sales order line items");
                throw;
            }
        }
    }
}
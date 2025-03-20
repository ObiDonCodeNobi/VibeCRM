using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.SalesOrderLineItem.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Queries.GetSalesOrderLineItemById
{
    /// <summary>
    /// Handler for processing the GetSalesOrderLineItemByIdQuery
    /// </summary>
    public class GetSalesOrderLineItemByIdQueryHandler : IRequestHandler<GetSalesOrderLineItemByIdQuery, SalesOrderLineItemDetailsDto>
    {
        private readonly ISalesOrderLineItemRepository _salesOrderLineItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSalesOrderLineItemByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderLineItemByIdQueryHandler"/> class
        /// </summary>
        /// <param name="salesOrderLineItemRepository">The sales order line item repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetSalesOrderLineItemByIdQueryHandler(
            ISalesOrderLineItemRepository salesOrderLineItemRepository,
            IMapper mapper,
            ILogger<GetSalesOrderLineItemByIdQueryHandler> logger)
        {
            _salesOrderLineItemRepository = salesOrderLineItemRepository ?? throw new ArgumentNullException(nameof(salesOrderLineItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetSalesOrderLineItemByIdQuery by retrieving a sales order line item by its ID from the database.
        /// </summary>
        /// <param name="request">The query containing the ID of the sales order line item to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A SalesOrderLineItemDetailsDto representing the requested sales order line item, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<SalesOrderLineItemDetailsDto> Handle(GetSalesOrderLineItemByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving sales order line item with ID {SalesOrderLineItemId}", request.Id);

            try
            {
                var salesOrderLineItem = await _salesOrderLineItemRepository.GetByIdAsync(request.Id, cancellationToken);

                if (salesOrderLineItem == null)
                {
                    _logger.LogWarning("Sales order line item with ID {SalesOrderLineItemId} not found", request.Id);
                    return new SalesOrderLineItemDetailsDto();
                }

                if (!salesOrderLineItem.Active)
                {
                    _logger.LogWarning("Sales order line item with ID {SalesOrderLineItemId} is inactive", request.Id);
                    return new SalesOrderLineItemDetailsDto();
                }

                _logger.LogInformation("Successfully retrieved sales order line item with ID {SalesOrderLineItemId}", request.Id);

                return _mapper.Map<SalesOrderLineItemDetailsDto>(salesOrderLineItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sales order line item with ID {SalesOrderLineItemId}", request.Id);
                throw;
            }
        }
    }
}
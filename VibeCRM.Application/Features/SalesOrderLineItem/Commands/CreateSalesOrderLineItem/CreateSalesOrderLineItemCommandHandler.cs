using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.SalesOrderLineItem;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Commands.CreateSalesOrderLineItem
{
    /// <summary>
    /// Handler for processing the CreateSalesOrderLineItemCommand
    /// </summary>
    public class CreateSalesOrderLineItemCommandHandler : IRequestHandler<CreateSalesOrderLineItemCommand, SalesOrderLineItemDetailsDto>
    {
        private readonly ISalesOrderLineItemRepository _salesOrderLineItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSalesOrderLineItemCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSalesOrderLineItemCommandHandler"/> class
        /// </summary>
        /// <param name="salesOrderLineItemRepository">The sales order line item repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public CreateSalesOrderLineItemCommandHandler(
            ISalesOrderLineItemRepository salesOrderLineItemRepository,
            IMapper mapper,
            ILogger<CreateSalesOrderLineItemCommandHandler> logger)
        {
            _salesOrderLineItemRepository = salesOrderLineItemRepository ?? throw new ArgumentNullException(nameof(salesOrderLineItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateSalesOrderLineItemCommand
        /// </summary>
        /// <param name="request">The command to create a sales order line item</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The created sales order line item details</returns>
        public async Task<SalesOrderLineItemDetailsDto> Handle(CreateSalesOrderLineItemCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating new sales order line item for sales order {SalesOrderId}", request.SalesOrderId);

            try
            {
                var salesOrderLineItem = _mapper.Map<Domain.Entities.BusinessEntities.SalesOrderLineItem>(request);

                // Set audit properties
                salesOrderLineItem.CreatedDate = DateTime.UtcNow;
                salesOrderLineItem.ModifiedDate = DateTime.UtcNow;

                // Ensure Active is set to true for new entities
                salesOrderLineItem.Active = true;

                var createdSalesOrderLineItem = await _salesOrderLineItemRepository.AddAsync(salesOrderLineItem, cancellationToken);

                _logger.LogInformation("Successfully created sales order line item with ID {Id}", createdSalesOrderLineItem.Id);

                return _mapper.Map<SalesOrderLineItemDetailsDto>(createdSalesOrderLineItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating sales order line item for sales order {SalesOrderId}", request.SalesOrderId);
                throw;
            }
        }
    }
}
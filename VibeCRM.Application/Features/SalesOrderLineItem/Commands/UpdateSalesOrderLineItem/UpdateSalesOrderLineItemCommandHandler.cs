using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.SalesOrderLineItem.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Commands.UpdateSalesOrderLineItem
{
    /// <summary>
    /// Handler for processing the UpdateSalesOrderLineItemCommand
    /// </summary>
    public class UpdateSalesOrderLineItemCommandHandler : IRequestHandler<UpdateSalesOrderLineItemCommand, SalesOrderLineItemDetailsDto>
    {
        private readonly ISalesOrderLineItemRepository _salesOrderLineItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateSalesOrderLineItemCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSalesOrderLineItemCommandHandler"/> class
        /// </summary>
        /// <param name="salesOrderLineItemRepository">The sales order line item repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public UpdateSalesOrderLineItemCommandHandler(
            ISalesOrderLineItemRepository salesOrderLineItemRepository,
            IMapper mapper,
            ILogger<UpdateSalesOrderLineItemCommandHandler> logger)
        {
            _salesOrderLineItemRepository = salesOrderLineItemRepository ?? throw new ArgumentNullException(nameof(salesOrderLineItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateSalesOrderLineItemCommand
        /// </summary>
        /// <param name="request">The command to update a sales order line item</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The updated sales order line item details</returns>
        public async Task<SalesOrderLineItemDetailsDto> Handle(UpdateSalesOrderLineItemCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating sales order line item with ID {SalesOrderLineItemId}", request.Id);

            try
            {
                // Get the existing entity
                var existingLineItem = await _salesOrderLineItemRepository.GetByIdAsync(request.Id, cancellationToken);

                if (existingLineItem == null)
                {
                    _logger.LogWarning("Sales order line item with ID {SalesOrderLineItemId} not found", request.Id);
                    throw new InvalidOperationException($"Sales order line item with ID {request.Id} not found");
                }

                if (!existingLineItem.Active)
                {
                    _logger.LogWarning("Cannot update inactive sales order line item with ID {SalesOrderLineItemId}", request.Id);
                    throw new InvalidOperationException($"Cannot update inactive sales order line item with ID {request.Id}");
                }

                // Map the updated values to the existing entity
                _mapper.Map(request, existingLineItem);

                // Update the modified date
                existingLineItem.ModifiedDate = DateTime.UtcNow;

                // Ensure Active remains true
                existingLineItem.Active = true;

                var updatedLineItem = await _salesOrderLineItemRepository.UpdateAsync(existingLineItem, cancellationToken);

                _logger.LogInformation("Successfully updated sales order line item with ID {SalesOrderLineItemId}", updatedLineItem.SalesOrderLineItemId);

                return _mapper.Map<SalesOrderLineItemDetailsDto>(updatedLineItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating sales order line item with ID {SalesOrderLineItemId}", request.Id);
                throw;
            }
        }
    }
}
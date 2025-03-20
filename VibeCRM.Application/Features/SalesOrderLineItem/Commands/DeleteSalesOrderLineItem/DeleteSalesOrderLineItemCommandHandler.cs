using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.SalesOrderLineItem.Commands.DeleteSalesOrderLineItem
{
    /// <summary>
    /// Handler for processing the DeleteSalesOrderLineItemCommand
    /// </summary>
    public class DeleteSalesOrderLineItemCommandHandler : IRequestHandler<DeleteSalesOrderLineItemCommand, bool>
    {
        private readonly ISalesOrderLineItemRepository _salesOrderLineItemRepository;
        private readonly ILogger<DeleteSalesOrderLineItemCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSalesOrderLineItemCommandHandler"/> class
        /// </summary>
        /// <param name="salesOrderLineItemRepository">The sales order line item repository</param>
        /// <param name="logger">The logger</param>
        public DeleteSalesOrderLineItemCommandHandler(
            ISalesOrderLineItemRepository salesOrderLineItemRepository,
            ILogger<DeleteSalesOrderLineItemCommandHandler> logger)
        {
            _salesOrderLineItemRepository = salesOrderLineItemRepository ?? throw new ArgumentNullException(nameof(salesOrderLineItemRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteSalesOrderLineItemCommand
        /// </summary>
        /// <param name="request">The command to delete a sales order line item</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>True if the sales order line item was successfully deleted, otherwise false</returns>
        public async Task<bool> Handle(DeleteSalesOrderLineItemCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting sales order line item with ID {SalesOrderLineItemId}", request.Id);

            try
            {
                // Get the existing entity to verify it exists and is active
                var existingLineItem = await _salesOrderLineItemRepository.GetByIdAsync(request.Id, cancellationToken);

                if (existingLineItem == null)
                {
                    _logger.LogWarning("Sales order line item with ID {SalesOrderLineItemId} not found", request.Id);
                    return false;
                }

                if (!existingLineItem.Active)
                {
                    _logger.LogWarning("Sales order line item with ID {SalesOrderLineItemId} is already inactive", request.Id);
                    return false;
                }

                // Perform soft delete by setting Active = false
                var result = await _salesOrderLineItemRepository.DeleteAsync(request.Id, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Successfully deleted (soft delete) sales order line item with ID {SalesOrderLineItemId}", request.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to delete sales order line item with ID {SalesOrderLineItemId}", request.Id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting sales order line item with ID {SalesOrderLineItemId}", request.Id);
                throw;
            }
        }
    }
}
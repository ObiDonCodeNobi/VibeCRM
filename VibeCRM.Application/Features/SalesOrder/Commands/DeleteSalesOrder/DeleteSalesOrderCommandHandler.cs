using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.SalesOrder.Commands.DeleteSalesOrder
{
    /// <summary>
    /// Handler for processing the DeleteSalesOrderCommand
    /// </summary>
    public class DeleteSalesOrderCommandHandler : IRequestHandler<DeleteSalesOrderCommand, bool>
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly ILogger<DeleteSalesOrderCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSalesOrderCommandHandler"/> class.
        /// </summary>
        /// <param name="salesOrderRepository">The sales order repository for data access operations</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public DeleteSalesOrderCommandHandler(
            ISalesOrderRepository salesOrderRepository,
            ILogger<DeleteSalesOrderCommandHandler> logger)
        {
            _salesOrderRepository = salesOrderRepository ?? throw new ArgumentNullException(nameof(salesOrderRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteSalesOrderCommand by performing a soft delete on an existing sales order
        /// </summary>
        /// <param name="request">The command containing the ID of the sales order to delete</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>True if the sales order was successfully deleted, otherwise false</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when the sales order could not be found or deleted</exception>
        public async Task<bool> Handle(DeleteSalesOrderCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            try
            {
                _logger.LogInformation("Soft deleting sales order with ID {SalesOrderId}", request.Id);

                // Get the existing sales order
                var existingSalesOrder = await _salesOrderRepository.GetByIdAsync(request.Id, cancellationToken);

                if (existingSalesOrder == null)
                {
                    throw new InvalidOperationException($"Sales order with ID {request.Id} not found");
                }

                // Update the ModifiedBy property before deletion
                existingSalesOrder.ModifiedBy = request.ModifiedBy;
                existingSalesOrder.ModifiedDate = DateTime.UtcNow;

                // Perform soft delete (sets Active = 0)
                var result = await _salesOrderRepository.DeleteAsync(request.Id, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Successfully soft deleted sales order with ID {SalesOrderId}", request.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to soft delete sales order with ID {SalesOrderId}", request.Id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error soft deleting sales order with ID {SalesOrderId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.SalesOrderStatus.Commands.DeleteSalesOrderStatus
{
    /// <summary>
    /// Handler for the DeleteSalesOrderStatusCommand.
    /// Soft-deletes an existing sales order status in the database.
    /// </summary>
    public class DeleteSalesOrderStatusCommandHandler : IRequestHandler<DeleteSalesOrderStatusCommand, bool>
    {
        private readonly ISalesOrderStatusRepository _salesOrderStatusRepository;
        private readonly ILogger<DeleteSalesOrderStatusCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSalesOrderStatusCommandHandler"/> class.
        /// </summary>
        /// <param name="salesOrderStatusRepository">The sales order status repository.</param>
        /// <param name="logger">The logger.</param>
        public DeleteSalesOrderStatusCommandHandler(
            ISalesOrderStatusRepository salesOrderStatusRepository,
            ILogger<DeleteSalesOrderStatusCommandHandler> logger)
        {
            _salesOrderStatusRepository = salesOrderStatusRepository ?? throw new ArgumentNullException(nameof(salesOrderStatusRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteSalesOrderStatusCommand by soft-deleting an existing sales order status in the database.
        /// Sets the Active property to false rather than physically removing the record.
        /// </summary>
        /// <param name="request">The command request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the sales order status was successfully deleted, otherwise false.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the sales order status to delete is not found.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the deletion process.</exception>
        public async Task<bool> Handle(DeleteSalesOrderStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Soft-deleting sales order status with ID: {SalesOrderStatusId}", request.Id);

                // Check if sales order status exists
                var existingSalesOrderStatus = await _salesOrderStatusRepository.GetByIdAsync(request.Id, cancellationToken);

                if (existingSalesOrderStatus == null)
                {
                    _logger.LogError("Sales order status with ID {SalesOrderStatusId} not found", request.Id);
                    throw new KeyNotFoundException($"Sales order status with ID {request.Id} not found");
                }

                // Delete sales order status (soft delete) - pass the ID, not the entity
                var result = await _salesOrderStatusRepository.DeleteAsync(request.Id, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Successfully soft-deleted sales order status with ID: {SalesOrderStatusId}", request.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to soft-delete sales order status with ID: {SalesOrderStatusId}", request.Id);
                }

                return result;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error soft-deleting sales order status: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
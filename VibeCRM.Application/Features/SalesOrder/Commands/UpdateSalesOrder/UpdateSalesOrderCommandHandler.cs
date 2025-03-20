using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.SalesOrder.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.SalesOrder.Commands.UpdateSalesOrder
{
    /// <summary>
    /// Handler for processing the UpdateSalesOrderCommand
    /// </summary>
    public class UpdateSalesOrderCommandHandler : IRequestHandler<UpdateSalesOrderCommand, SalesOrderDto>
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateSalesOrderCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSalesOrderCommandHandler"/> class.
        /// </summary>
        /// <param name="salesOrderRepository">The sales order repository for data access operations</param>
        /// <param name="mapper">The AutoMapper instance for object mapping</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public UpdateSalesOrderCommandHandler(
            ISalesOrderRepository salesOrderRepository,
            IMapper mapper,
            ILogger<UpdateSalesOrderCommandHandler> logger)
        {
            _salesOrderRepository = salesOrderRepository ?? throw new ArgumentNullException(nameof(salesOrderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateSalesOrderCommand by updating an existing sales order in the database
        /// </summary>
        /// <param name="request">The command containing the sales order details to update</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The updated sales order DTO</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when the sales order could not be found or updated</exception>
        public async Task<SalesOrderDto> Handle(UpdateSalesOrderCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            try
            {
                _logger.LogInformation("Updating sales order with ID {SalesOrderId}", request.Id);

                // Get the existing sales order
                var existingSalesOrder = await _salesOrderRepository.GetByIdAsync(request.Id, cancellationToken);

                if (existingSalesOrder == null)
                {
                    throw new InvalidOperationException($"Sales order with ID {request.Id} not found");
                }

                // Map command to entity, preserving original values not in the command
                var salesOrderToUpdate = _mapper.Map<UpdateSalesOrderCommand, Domain.Entities.BusinessEntities.SalesOrder>(request, existingSalesOrder);
                salesOrderToUpdate.ModifiedDate = DateTime.UtcNow;
                salesOrderToUpdate.ModifiedBy = request.ModifiedBy;

                // Update the sales order in the repository
                var updatedSalesOrder = await _salesOrderRepository.UpdateAsync(salesOrderToUpdate, cancellationToken);

                if (updatedSalesOrder == null)
                {
                    throw new InvalidOperationException($"Failed to update sales order with ID {request.Id}");
                }

                _logger.LogInformation("Successfully updated sales order with ID {SalesOrderId}", updatedSalesOrder.SalesOrderId);

                // Map the updated entity back to DTO
                return _mapper.Map<SalesOrderDto>(updatedSalesOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating sales order with ID {SalesOrderId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
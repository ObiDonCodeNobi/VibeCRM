using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.SalesOrderStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.SalesOrderStatus.Commands.UpdateSalesOrderStatus
{
    /// <summary>
    /// Handler for the UpdateSalesOrderStatusCommand.
    /// Updates an existing sales order status in the database.
    /// </summary>
    public class UpdateSalesOrderStatusCommandHandler : IRequestHandler<UpdateSalesOrderStatusCommand, SalesOrderStatusDto>
    {
        private readonly ISalesOrderStatusRepository _salesOrderStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateSalesOrderStatusCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSalesOrderStatusCommandHandler"/> class.
        /// </summary>
        /// <param name="salesOrderStatusRepository">The sales order status repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public UpdateSalesOrderStatusCommandHandler(
            ISalesOrderStatusRepository salesOrderStatusRepository,
            IMapper mapper,
            ILogger<UpdateSalesOrderStatusCommandHandler> logger)
        {
            _salesOrderStatusRepository = salesOrderStatusRepository ?? throw new ArgumentNullException(nameof(salesOrderStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateSalesOrderStatusCommand by updating an existing sales order status in the database.
        /// </summary>
        /// <param name="request">The command request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated sales order status DTO.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the sales order status to update is not found.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the update process.</exception>
        public async Task<SalesOrderStatusDto> Handle(UpdateSalesOrderStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating sales order status with ID: {SalesOrderStatusId}", request.Id);

                // Get existing sales order status
                var existingSalesOrderStatus = await _salesOrderStatusRepository.GetByIdAsync(request.Id, cancellationToken);

                if (existingSalesOrderStatus == null)
                {
                    _logger.LogError("Sales order status with ID {SalesOrderStatusId} not found", request.Id);
                    throw new KeyNotFoundException($"Sales order status with ID {request.Id} not found");
                }

                // Update properties
                existingSalesOrderStatus.Status = request.Status;
                existingSalesOrderStatus.Description = request.Description;
                existingSalesOrderStatus.OrdinalPosition = request.OrdinalPosition;
                existingSalesOrderStatus.ModifiedDate = DateTime.UtcNow;
                // ModifiedBy should be set to the current user's ID when authentication is implemented
                // Not setting ModifiedBy here as it requires a Guid, not a string

                // Update in repository
                var updatedSalesOrderStatus = await _salesOrderStatusRepository.UpdateAsync(existingSalesOrderStatus, cancellationToken);

                // Map to DTO
                var salesOrderStatusDto = _mapper.Map<SalesOrderStatusDto>(updatedSalesOrderStatus);

                _logger.LogInformation("Successfully updated sales order status with ID: {SalesOrderStatusId}", salesOrderStatusDto.Id);

                return salesOrderStatusDto;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating sales order status: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
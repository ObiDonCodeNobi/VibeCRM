using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.SalesOrderStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.SalesOrderStatus.Queries.GetSalesOrderStatusById
{
    /// <summary>
    /// Handler for the GetSalesOrderStatusByIdQuery.
    /// Retrieves a specific sales order status by its ID from the database.
    /// </summary>
    public class GetSalesOrderStatusByIdQueryHandler : IRequestHandler<GetSalesOrderStatusByIdQuery, SalesOrderStatusDetailsDto>
    {
        private readonly ISalesOrderStatusRepository _salesOrderStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSalesOrderStatusByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderStatusByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="salesOrderStatusRepository">The sales order status repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetSalesOrderStatusByIdQueryHandler(
            ISalesOrderStatusRepository salesOrderStatusRepository,
            IMapper mapper,
            ILogger<GetSalesOrderStatusByIdQueryHandler> logger)
        {
            _salesOrderStatusRepository = salesOrderStatusRepository ?? throw new ArgumentNullException(nameof(salesOrderStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetSalesOrderStatusByIdQuery by retrieving a specific sales order status by its ID.
        /// </summary>
        /// <param name="request">The query request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The sales order status details DTO if found, otherwise null.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the sales order status with the specified ID is not found.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<SalesOrderStatusDetailsDto> Handle(GetSalesOrderStatusByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving sales order status with ID: {SalesOrderStatusId}", request.Id);

                // Get sales order status by ID
                var salesOrderStatus = await _salesOrderStatusRepository.GetByIdAsync(request.Id, cancellationToken);
                
                if (salesOrderStatus == null)
                {
                    _logger.LogError("Sales order status with ID {SalesOrderStatusId} not found", request.Id);
                    throw new KeyNotFoundException($"Sales order status with ID {request.Id} not found");
                }

                // Map to DTO
                var salesOrderStatusDto = _mapper.Map<SalesOrderStatusDetailsDto>(salesOrderStatus);

                // Note: In a real-world scenario, you might want to populate the SalesOrderCount property
                // by querying the sales order repository to get the count for this status

                _logger.LogInformation("Successfully retrieved sales order status with ID: {SalesOrderStatusId}", request.Id);

                return salesOrderStatusDto;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sales order status by ID: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}

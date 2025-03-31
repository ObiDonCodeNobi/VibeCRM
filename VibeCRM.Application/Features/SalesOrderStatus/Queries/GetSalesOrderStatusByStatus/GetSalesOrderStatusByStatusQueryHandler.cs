using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.SalesOrderStatus;

namespace VibeCRM.Application.Features.SalesOrderStatus.Queries.GetSalesOrderStatusByStatus
{
    /// <summary>
    /// Handler for the GetSalesOrderStatusByStatusQuery.
    /// Retrieves sales order statuses by their status name from the database.
    /// </summary>
    public class GetSalesOrderStatusByStatusQueryHandler : IRequestHandler<GetSalesOrderStatusByStatusQuery, IEnumerable<SalesOrderStatusDto>>
    {
        private readonly ISalesOrderStatusRepository _salesOrderStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSalesOrderStatusByStatusQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderStatusByStatusQueryHandler"/> class.
        /// </summary>
        /// <param name="salesOrderStatusRepository">The sales order status repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetSalesOrderStatusByStatusQueryHandler(
            ISalesOrderStatusRepository salesOrderStatusRepository,
            IMapper mapper,
            ILogger<GetSalesOrderStatusByStatusQueryHandler> logger)
        {
            _salesOrderStatusRepository = salesOrderStatusRepository ?? throw new ArgumentNullException(nameof(salesOrderStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetSalesOrderStatusByStatusQuery by retrieving sales order statuses by their status name.
        /// </summary>
        /// <param name="request">The query request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of sales order status DTOs with the specified status name.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<IEnumerable<SalesOrderStatusDto>> Handle(GetSalesOrderStatusByStatusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving sales order statuses with status name: {StatusName}", request.Status);

                // Get sales order statuses by status name
                var salesOrderStatuses = await _salesOrderStatusRepository.GetByStatusAsync(request.Status, cancellationToken);

                // Map to DTOs
                var salesOrderStatusDtos = _mapper.Map<IEnumerable<SalesOrderStatusDto>>(salesOrderStatuses);

                _logger.LogInformation("Successfully retrieved {Count} sales order statuses with status name {StatusName}",
                    salesOrderStatusDtos.Count(), request.Status);

                return salesOrderStatusDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sales order statuses by status name: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
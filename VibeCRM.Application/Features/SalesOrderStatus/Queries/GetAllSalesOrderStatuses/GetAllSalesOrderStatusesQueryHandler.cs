using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.SalesOrderStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.SalesOrderStatus.Queries.GetAllSalesOrderStatuses
{
    /// <summary>
    /// Handler for the GetAllSalesOrderStatusesQuery.
    /// Retrieves all active sales order statuses from the database.
    /// </summary>
    public class GetAllSalesOrderStatusesQueryHandler : IRequestHandler<GetAllSalesOrderStatusesQuery, IEnumerable<SalesOrderStatusListDto>>
    {
        private readonly ISalesOrderStatusRepository _salesOrderStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllSalesOrderStatusesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllSalesOrderStatusesQueryHandler"/> class.
        /// </summary>
        /// <param name="salesOrderStatusRepository">The sales order status repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetAllSalesOrderStatusesQueryHandler(
            ISalesOrderStatusRepository salesOrderStatusRepository,
            IMapper mapper,
            ILogger<GetAllSalesOrderStatusesQueryHandler> logger)
        {
            _salesOrderStatusRepository = salesOrderStatusRepository ?? throw new ArgumentNullException(nameof(salesOrderStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllSalesOrderStatusesQuery by retrieving all active sales order statuses from the database.
        /// </summary>
        /// <param name="request">The query request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of sales order status list DTOs.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<IEnumerable<SalesOrderStatusListDto>> Handle(GetAllSalesOrderStatusesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving all active sales order statuses");

                // Get all active sales order statuses
                var salesOrderStatuses = await _salesOrderStatusRepository.GetAllAsync(cancellationToken);
                
                // Map to DTOs
                var salesOrderStatusDtos = _mapper.Map<IEnumerable<SalesOrderStatusListDto>>(salesOrderStatuses);

                // Note: In a real-world scenario, you might want to populate the SalesOrderCount property
                // by querying the sales order repository to get counts for each status

                _logger.LogInformation("Successfully retrieved {Count} sales order statuses", salesOrderStatusDtos.Count());

                return salesOrderStatusDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all sales order statuses: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.SalesOrderStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.SalesOrderStatus.Queries.GetSalesOrderStatusByOrdinalPosition
{
    /// <summary>
    /// Handler for the GetSalesOrderStatusByOrdinalPositionQuery.
    /// Retrieves sales order statuses by their ordinal position from the database.
    /// </summary>
    public class GetSalesOrderStatusByOrdinalPositionQueryHandler : IRequestHandler<GetSalesOrderStatusByOrdinalPositionQuery, IEnumerable<SalesOrderStatusDto>>
    {
        private readonly ISalesOrderStatusRepository _salesOrderStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSalesOrderStatusByOrdinalPositionQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderStatusByOrdinalPositionQueryHandler"/> class.
        /// </summary>
        /// <param name="salesOrderStatusRepository">The sales order status repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetSalesOrderStatusByOrdinalPositionQueryHandler(
            ISalesOrderStatusRepository salesOrderStatusRepository,
            IMapper mapper,
            ILogger<GetSalesOrderStatusByOrdinalPositionQueryHandler> logger)
        {
            _salesOrderStatusRepository = salesOrderStatusRepository ?? throw new ArgumentNullException(nameof(salesOrderStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetSalesOrderStatusByOrdinalPositionQuery by retrieving sales order statuses by their ordinal position.
        /// </summary>
        /// <param name="request">The query request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of sales order status DTOs with the specified ordinal position.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<IEnumerable<SalesOrderStatusDto>> Handle(GetSalesOrderStatusByOrdinalPositionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving sales order statuses with ordinal position: {OrdinalPosition}", request.OrdinalPosition);

                // Get sales order statuses by ordinal position
                var salesOrderStatuses = await _salesOrderStatusRepository.GetByOrdinalPositionAsync(cancellationToken);

                // Filter by the requested ordinal position
                var filteredSalesOrderStatuses = salesOrderStatuses.Where(sos => sos.OrdinalPosition == request.OrdinalPosition);

                // Map to DTOs
                var salesOrderStatusDtos = _mapper.Map<IEnumerable<SalesOrderStatusDto>>(filteredSalesOrderStatuses);

                _logger.LogInformation("Successfully retrieved {Count} sales order statuses with ordinal position {OrdinalPosition}",
                    salesOrderStatusDtos.Count(), request.OrdinalPosition);

                return salesOrderStatusDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sales order statuses by ordinal position: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
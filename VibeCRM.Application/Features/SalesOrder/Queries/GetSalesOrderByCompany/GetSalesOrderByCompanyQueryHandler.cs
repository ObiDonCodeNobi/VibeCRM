using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.SalesOrder.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.SalesOrder.Queries.GetSalesOrderByCompany
{
    /// <summary>
    /// Handler for processing the GetSalesOrderByCompanyQuery
    /// </summary>
    public class GetSalesOrderByCompanyQueryHandler : IRequestHandler<GetSalesOrderByCompanyQuery, IEnumerable<SalesOrderListDto>>
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSalesOrderByCompanyQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesOrderByCompanyQueryHandler"/> class.
        /// </summary>
        /// <param name="salesOrderRepository">The sales order repository for data access operations</param>
        /// <param name="mapper">The AutoMapper instance for object mapping</param>
        /// <param name="logger">The logger for logging events and errors</param>
        public GetSalesOrderByCompanyQueryHandler(
            ISalesOrderRepository salesOrderRepository,
            IMapper mapper,
            ILogger<GetSalesOrderByCompanyQueryHandler> logger)
        {
            _salesOrderRepository = salesOrderRepository ?? throw new ArgumentNullException(nameof(salesOrderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetSalesOrderByCompanyQuery by retrieving all sales orders associated with a specific company
        /// </summary>
        /// <param name="request">The query containing the company ID to filter sales orders by</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of sales order list DTOs associated with the specified company</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
        public async Task<IEnumerable<SalesOrderListDto>> Handle(GetSalesOrderByCompanyQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            try
            {
                _logger.LogInformation("Retrieving sales orders for company with ID {CompanyId}", request.CompanyId);

                // Get the sales orders from the repository
                var salesOrders = await _salesOrderRepository.GetByCompanyAsync(request.CompanyId, cancellationToken);

                // Map the entities to DTOs
                var salesOrderDtos = _mapper.Map<IEnumerable<SalesOrderListDto>>(salesOrders);

                _logger.LogInformation("Successfully retrieved sales orders for company with ID {CompanyId}", request.CompanyId);

                return salesOrderDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sales orders for company with ID {CompanyId}: {ErrorMessage}", request.CompanyId, ex.Message);
                throw;
            }
        }
    }
}
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Invoice.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Invoice.Queries.GetInvoicesBySalesOrderId
{
    /// <summary>
    /// Handler for processing the GetInvoicesBySalesOrderIdQuery
    /// </summary>
    public class GetInvoicesBySalesOrderIdQueryHandler : IRequestHandler<GetInvoicesBySalesOrderIdQuery, IEnumerable<InvoiceListDto>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetInvoicesBySalesOrderIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetInvoicesBySalesOrderIdQueryHandler"/> class
        /// </summary>
        /// <param name="invoiceRepository">The invoice repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger instance</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null</exception>
        public GetInvoicesBySalesOrderIdQueryHandler(
            IInvoiceRepository invoiceRepository,
            IMapper mapper,
            ILogger<GetInvoicesBySalesOrderIdQueryHandler> logger)
        {
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetInvoicesBySalesOrderIdQuery to retrieve invoices by their sales order ID
        /// </summary>
        /// <param name="request">The query containing the sales order ID to retrieve invoices for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoices associated with the specified sales order</returns>
        /// <exception cref="InvalidOperationException">Thrown when the retrieval operation fails</exception>
        public async Task<IEnumerable<InvoiceListDto>> Handle(GetInvoicesBySalesOrderIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving invoices for sales order with ID {SalesOrderId}", request.SalesOrderId);

                // Get invoices by sales order ID from the repository
                var invoices = await _invoiceRepository.GetBySalesOrderIdAsync(request.SalesOrderId, cancellationToken);

                // Map to DTOs
                var invoiceDtos = _mapper.Map<IEnumerable<InvoiceListDto>>(invoices);

                _logger.LogInformation("Successfully retrieved invoices for sales order with ID {SalesOrderId}", request.SalesOrderId);

                return invoiceDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving invoices for sales order with ID {SalesOrderId}", request.SalesOrderId);
                throw new InvalidOperationException($"Failed to retrieve invoices: {ex.Message}", ex);
            }
        }
    }
}
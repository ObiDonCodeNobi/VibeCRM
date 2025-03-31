using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Invoice;

namespace VibeCRM.Application.Features.Invoice.Queries.GetAllInvoices
{
    /// <summary>
    /// Handler for processing the GetAllInvoicesQuery
    /// </summary>
    public class GetAllInvoicesQueryHandler : IRequestHandler<GetAllInvoicesQuery, IEnumerable<InvoiceListDto>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllInvoicesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllInvoicesQueryHandler"/> class
        /// </summary>
        /// <param name="invoiceRepository">The invoice repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger instance</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null</exception>
        public GetAllInvoicesQueryHandler(
            IInvoiceRepository invoiceRepository,
            IMapper mapper,
            ILogger<GetAllInvoicesQueryHandler> logger)
        {
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllInvoicesQuery to retrieve all invoices
        /// </summary>
        /// <param name="request">The query for retrieving all invoices</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoices</returns>
        /// <exception cref="InvalidOperationException">Thrown when the retrieval operation fails</exception>
        public async Task<IEnumerable<InvoiceListDto>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving all invoices");

                // Get all invoices from the repository
                var invoices = await _invoiceRepository.GetAllAsync(cancellationToken);

                // Map to DTOs
                var invoiceDtos = _mapper.Map<IEnumerable<InvoiceListDto>>(invoices);

                _logger.LogInformation("Successfully retrieved all invoices");

                return invoiceDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all invoices");
                throw new InvalidOperationException($"Failed to retrieve invoices: {ex.Message}", ex);
            }
        }
    }
}
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Invoice;

namespace VibeCRM.Application.Features.Invoice.Queries.GetInvoiceById
{
    /// <summary>
    /// Handler for processing the GetInvoiceByIdQuery
    /// </summary>
    public class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery, InvoiceDetailsDto>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetInvoiceByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetInvoiceByIdQueryHandler"/> class
        /// </summary>
        /// <param name="invoiceRepository">The invoice repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger instance</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null</exception>
        public GetInvoiceByIdQueryHandler(
            IInvoiceRepository invoiceRepository,
            IMapper mapper,
            ILogger<GetInvoiceByIdQueryHandler> logger)
        {
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetInvoiceByIdQuery to retrieve an invoice by its ID
        /// </summary>
        /// <param name="request">The query containing the invoice ID to retrieve</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The invoice details if found; otherwise, null</returns>
        /// <exception cref="InvalidOperationException">Thrown when the retrieval operation fails</exception>
        public async Task<InvoiceDetailsDto> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving invoice with ID {InvoiceId}", request.Id);

                // Get the invoice from the repository
                var invoice = await _invoiceRepository.GetByIdAsync(request.Id, cancellationToken);

                if (invoice == null)
                {
                    _logger.LogWarning("Invoice with ID {InvoiceId} not found", request.Id);
                    return new InvoiceDetailsDto();
                }

                // Map to DTO
                var invoiceDto = _mapper.Map<InvoiceDetailsDto>(invoice);

                _logger.LogInformation("Successfully retrieved invoice with ID {InvoiceId}", request.Id);

                return invoiceDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving invoice with ID {InvoiceId}", request.Id);
                throw new InvalidOperationException($"Failed to retrieve invoice: {ex.Message}", ex);
            }
        }
    }
}
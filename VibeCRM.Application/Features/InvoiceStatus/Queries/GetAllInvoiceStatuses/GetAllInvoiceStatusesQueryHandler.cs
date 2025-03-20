using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.InvoiceStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.InvoiceStatus.Queries.GetAllInvoiceStatuses
{
    /// <summary>
    /// Handler for the GetAllInvoiceStatusesQuery
    /// </summary>
    public class GetAllInvoiceStatusesQueryHandler : IRequestHandler<GetAllInvoiceStatusesQuery, IEnumerable<InvoiceStatusListDto>>
    {
        private readonly IInvoiceStatusRepository _invoiceStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllInvoiceStatusesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the GetAllInvoiceStatusesQueryHandler class
        /// </summary>
        /// <param name="invoiceStatusRepository">The invoice status repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetAllInvoiceStatusesQueryHandler(
            IInvoiceStatusRepository invoiceStatusRepository,
            IMapper mapper,
            ILogger<GetAllInvoiceStatusesQueryHandler> logger)
        {
            _invoiceStatusRepository = invoiceStatusRepository ?? throw new ArgumentNullException(nameof(invoiceStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllInvoiceStatusesQuery
        /// </summary>
        /// <param name="request">The query to retrieve all invoice statuses</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoice status list DTOs</returns>
        /// <exception cref="ApplicationException">Thrown when the invoice statuses could not be retrieved</exception>
        public async Task<IEnumerable<InvoiceStatusListDto>> Handle(GetAllInvoiceStatusesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving all invoice statuses");

                // Get invoice statuses ordered by ordinal position
                var invoiceStatuses = await _invoiceStatusRepository.GetByOrdinalPositionAsync(cancellationToken);

                // Map to DTOs
                var invoiceStatusDtos = _mapper.Map<IEnumerable<InvoiceStatusListDto>>(invoiceStatuses);

                // If invoice counts are requested, we would need to retrieve them from the invoice repository
                // This is a placeholder for future implementation
                if (request.IncludeInvoiceCounts)
                {
                    _logger.LogInformation("Invoice counts requested, but not implemented yet");
                    // TODO: Implement invoice count retrieval when Invoice entity is available
                }

                return invoiceStatusDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all invoice statuses: {ErrorMessage}", ex.Message);
                throw new ApplicationException($"Error retrieving all invoice statuses: {ex.Message}", ex);
            }
        }
    }
}

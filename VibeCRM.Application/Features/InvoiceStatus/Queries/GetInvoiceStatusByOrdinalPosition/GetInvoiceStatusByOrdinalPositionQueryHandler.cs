using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.InvoiceStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.InvoiceStatus.Queries.GetInvoiceStatusByOrdinalPosition
{
    /// <summary>
    /// Handler for the GetInvoiceStatusByOrdinalPositionQuery
    /// </summary>
    public class GetInvoiceStatusByOrdinalPositionQueryHandler : IRequestHandler<GetInvoiceStatusByOrdinalPositionQuery, IEnumerable<InvoiceStatusDto>>
    {
        private readonly IInvoiceStatusRepository _invoiceStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetInvoiceStatusByOrdinalPositionQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the GetInvoiceStatusByOrdinalPositionQueryHandler class
        /// </summary>
        /// <param name="invoiceStatusRepository">The invoice status repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetInvoiceStatusByOrdinalPositionQueryHandler(
            IInvoiceStatusRepository invoiceStatusRepository,
            IMapper mapper,
            ILogger<GetInvoiceStatusByOrdinalPositionQueryHandler> logger)
        {
            _invoiceStatusRepository = invoiceStatusRepository ?? throw new ArgumentNullException(nameof(invoiceStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetInvoiceStatusByOrdinalPositionQuery
        /// </summary>
        /// <param name="request">The query to retrieve invoice statuses ordered by ordinal position</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of invoice status DTOs ordered by ordinal position</returns>
        /// <exception cref="ApplicationException">Thrown when the invoice statuses could not be retrieved</exception>
        public async Task<IEnumerable<InvoiceStatusDto>> Handle(GetInvoiceStatusByOrdinalPositionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving invoice statuses ordered by ordinal position");

                // Get invoice statuses ordered by ordinal position
                var invoiceStatuses = await _invoiceStatusRepository.GetByOrdinalPositionAsync(cancellationToken);

                // Map to DTOs
                return _mapper.Map<IEnumerable<InvoiceStatusDto>>(invoiceStatuses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving invoice statuses by ordinal position: {ErrorMessage}", ex.Message);
                throw new ApplicationException($"Error retrieving invoice statuses by ordinal position: {ex.Message}", ex);
            }
        }
    }
}

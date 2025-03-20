using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.InvoiceStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.InvoiceStatus.Queries.GetInvoiceStatusByStatus
{
    /// <summary>
    /// Handler for the GetInvoiceStatusByStatusQuery
    /// </summary>
    public class GetInvoiceStatusByStatusQueryHandler : IRequestHandler<GetInvoiceStatusByStatusQuery, InvoiceStatusDto>
    {
        private readonly IInvoiceStatusRepository _invoiceStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetInvoiceStatusByStatusQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the GetInvoiceStatusByStatusQueryHandler class
        /// </summary>
        /// <param name="invoiceStatusRepository">The invoice status repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetInvoiceStatusByStatusQueryHandler(
            IInvoiceStatusRepository invoiceStatusRepository,
            IMapper mapper,
            ILogger<GetInvoiceStatusByStatusQueryHandler> logger)
        {
            _invoiceStatusRepository = invoiceStatusRepository ?? throw new ArgumentNullException(nameof(invoiceStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetInvoiceStatusByStatusQuery
        /// </summary>
        /// <param name="request">The query to retrieve an invoice status by its status name</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The invoice status DTO if found; otherwise, null</returns>
        /// <exception cref="ApplicationException">Thrown when the invoice status could not be retrieved</exception>
        public async Task<InvoiceStatusDto> Handle(GetInvoiceStatusByStatusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving invoice status with status name: {Status}", request.Status);

                if (string.IsNullOrWhiteSpace(request.Status))
                {
                    _logger.LogWarning("Status name is required");
                    return new InvoiceStatusDto();
                }

                // Get invoice status by status name
                var invoiceStatus = await _invoiceStatusRepository.GetByStatusAsync(request.Status, cancellationToken);
                if (invoiceStatus == null)
                {
                    _logger.LogWarning("Invoice status with status name {Status} not found", request.Status);
                    return new InvoiceStatusDto();
                }

                // Map to DTO
                return _mapper.Map<InvoiceStatusDto>(invoiceStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving invoice status by status name: {ErrorMessage}", ex.Message);
                throw new ApplicationException($"Error retrieving invoice status by status name: {ex.Message}", ex);
            }
        }
    }
}

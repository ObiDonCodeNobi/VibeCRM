using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.InvoiceStatus;

namespace VibeCRM.Application.Features.InvoiceStatus.Queries.GetDefaultInvoiceStatus
{
    /// <summary>
    /// Handler for the GetDefaultInvoiceStatusQuery
    /// </summary>
    public class GetDefaultInvoiceStatusQueryHandler : IRequestHandler<GetDefaultInvoiceStatusQuery, InvoiceStatusDto>
    {
        private readonly IInvoiceStatusRepository _invoiceStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDefaultInvoiceStatusQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the GetDefaultInvoiceStatusQueryHandler class
        /// </summary>
        /// <param name="invoiceStatusRepository">The invoice status repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetDefaultInvoiceStatusQueryHandler(
            IInvoiceStatusRepository invoiceStatusRepository,
            IMapper mapper,
            ILogger<GetDefaultInvoiceStatusQueryHandler> logger)
        {
            _invoiceStatusRepository = invoiceStatusRepository ?? throw new ArgumentNullException(nameof(invoiceStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetDefaultInvoiceStatusQuery
        /// </summary>
        /// <param name="request">The query to retrieve the default invoice status</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The default invoice status DTO if found; otherwise, null</returns>
        /// <exception cref="ApplicationException">Thrown when the default invoice status could not be retrieved</exception>
        public async Task<InvoiceStatusDto> Handle(GetDefaultInvoiceStatusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving default invoice status");

                // Get default invoice status
                var defaultInvoiceStatus = await _invoiceStatusRepository.GetDefaultAsync(cancellationToken);
                if (defaultInvoiceStatus == null)
                {
                    _logger.LogWarning("Default invoice status not found");
                    return new InvoiceStatusDto();
                }

                // Map to DTO
                return _mapper.Map<InvoiceStatusDto>(defaultInvoiceStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving default invoice status: {ErrorMessage}", ex.Message);
                throw new ApplicationException($"Error retrieving default invoice status: {ex.Message}", ex);
            }
        }
    }
}
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.InvoiceStatus;

namespace VibeCRM.Application.Features.InvoiceStatus.Queries.GetInvoiceStatusById
{
    /// <summary>
    /// Handler for the GetInvoiceStatusByIdQuery
    /// </summary>
    public class GetInvoiceStatusByIdQueryHandler : IRequestHandler<GetInvoiceStatusByIdQuery, InvoiceStatusDetailsDto>
    {
        private readonly IInvoiceStatusRepository _invoiceStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetInvoiceStatusByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the GetInvoiceStatusByIdQueryHandler class
        /// </summary>
        /// <param name="invoiceStatusRepository">The invoice status repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetInvoiceStatusByIdQueryHandler(
            IInvoiceStatusRepository invoiceStatusRepository,
            IMapper mapper,
            ILogger<GetInvoiceStatusByIdQueryHandler> logger)
        {
            _invoiceStatusRepository = invoiceStatusRepository ?? throw new ArgumentNullException(nameof(invoiceStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetInvoiceStatusByIdQuery
        /// </summary>
        /// <param name="request">The query to retrieve an invoice status by its ID</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>The invoice status details DTO if found; otherwise, null</returns>
        /// <exception cref="ApplicationException">Thrown when the invoice status could not be retrieved</exception>
        public async Task<InvoiceStatusDetailsDto> Handle(GetInvoiceStatusByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving invoice status with ID: {Id}", request.Id);

                // Get invoice status by ID
                var invoiceStatus = await _invoiceStatusRepository.GetByIdAsync(request.Id, cancellationToken);
                if (invoiceStatus == null)
                {
                    _logger.LogWarning("Invoice status with ID {Id} not found", request.Id);
                    return new InvoiceStatusDetailsDto();
                }

                // Map to DTO
                return _mapper.Map<InvoiceStatusDetailsDto>(invoiceStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving invoice status by ID: {ErrorMessage}", ex.Message);
                throw new ApplicationException($"Error retrieving invoice status by ID: {ex.Message}", ex);
            }
        }
    }
}
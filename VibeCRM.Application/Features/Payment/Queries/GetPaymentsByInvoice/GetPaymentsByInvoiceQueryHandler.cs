using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Payment;

namespace VibeCRM.Application.Features.Payment.Queries.GetPaymentsByInvoice
{
    /// <summary>
    /// Handler for processing GetPaymentsByInvoiceQuery requests.
    /// Implements the CQRS query handler pattern for retrieving payments associated with a specific invoice.
    /// </summary>
    public class GetPaymentsByInvoiceQueryHandler : IRequestHandler<GetPaymentsByInvoiceQuery, IEnumerable<PaymentListDto>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPaymentsByInvoiceQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentsByInvoiceQueryHandler"/> class.
        /// </summary>
        /// <param name="paymentRepository">The payment repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetPaymentsByInvoiceQueryHandler(
            IPaymentRepository paymentRepository,
            IMapper mapper,
            ILogger<GetPaymentsByInvoiceQueryHandler> logger)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPaymentsByInvoiceQuery by retrieving all payments associated with a specific invoice.
        /// </summary>
        /// <param name="request">The GetPaymentsByInvoiceQuery containing the invoice ID to retrieve payments for.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of PaymentListDto objects representing the payments associated with the invoice.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the invoice ID is empty.</exception>
        public async Task<IEnumerable<PaymentListDto>> Handle(GetPaymentsByInvoiceQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.InvoiceId == Guid.Empty) throw new ArgumentException("Invoice ID cannot be empty", nameof(request.InvoiceId));

            try
            {
                // Get payments by invoice ID (Active=1 filter is applied in the repository)
                var payments = await _paymentRepository.GetByInvoiceIdAsync(request.InvoiceId, cancellationToken);

                _logger.LogInformation("Retrieved {Count} payments for invoice with ID: {InvoiceId}",
                    payments is ICollection<Domain.Entities.BusinessEntities.Payment> collection ? collection.Count : "multiple",
                    request.InvoiceId);

                // Map entities to DTOs
                return _mapper.Map<IEnumerable<PaymentListDto>>(payments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving payments for invoice with ID {InvoiceId}: {ErrorMessage}",
                    request.InvoiceId, ex.Message);
                throw;
            }
        }
    }
}
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.PaymentLineItem;

namespace VibeCRM.Application.Features.PaymentLineItem.Queries.GetPaymentLineItemsByInvoiceId
{
    /// <summary>
    /// Handler for processing GetPaymentLineItemsByInvoiceIdQuery requests.
    /// </summary>
    /// <remarks>
    /// Implements the CQRS query handler pattern for retrieving payment line items by invoice ID.
    /// </remarks>
    public class GetPaymentLineItemsByInvoiceIdQueryHandler : IRequestHandler<GetPaymentLineItemsByInvoiceIdQuery, IEnumerable<PaymentLineItemListDto>>
    {
        private readonly IPaymentLineItemRepository _paymentLineItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPaymentLineItemsByInvoiceIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentLineItemsByInvoiceIdQueryHandler"/> class.
        /// </summary>
        /// <param name="paymentLineItemRepository">The payment line item repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetPaymentLineItemsByInvoiceIdQueryHandler(
            IPaymentLineItemRepository paymentLineItemRepository,
            IMapper mapper,
            ILogger<GetPaymentLineItemsByInvoiceIdQueryHandler> logger)
        {
            _paymentLineItemRepository = paymentLineItemRepository ?? throw new ArgumentNullException(nameof(paymentLineItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPaymentLineItemsByInvoiceIdQuery by retrieving payment line items for a specific invoice.
        /// </summary>
        /// <param name="request">The GetPaymentLineItemsByInvoiceIdQuery containing the invoice ID to retrieve payment line items for.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of PaymentLineItemListDto objects representing the payment line items for the specified invoice.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<IEnumerable<PaymentLineItemListDto>> Handle(GetPaymentLineItemsByInvoiceIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving payment line items for invoice with ID {InvoiceId}", request.InvoiceId);

                var paymentLineItems = await _paymentLineItemRepository.GetByInvoiceIdAsync(request.InvoiceId, cancellationToken);

                _logger.LogInformation("Successfully retrieved payment line items for invoice with ID {InvoiceId}", request.InvoiceId);

                return _mapper.Map<IEnumerable<PaymentLineItemListDto>>(paymentLineItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving payment line items for invoice with ID {InvoiceId}: {ErrorMessage}", request.InvoiceId, ex.Message);
                throw;
            }
        }
    }
}
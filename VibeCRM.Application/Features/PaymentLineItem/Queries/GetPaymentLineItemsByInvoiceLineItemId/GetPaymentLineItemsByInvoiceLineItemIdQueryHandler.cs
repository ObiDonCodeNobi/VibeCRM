using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.PaymentLineItem.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.PaymentLineItem.Queries.GetPaymentLineItemsByInvoiceLineItemId
{
    /// <summary>
    /// Handler for processing GetPaymentLineItemsByInvoiceLineItemIdQuery requests.
    /// </summary>
    /// <remarks>
    /// Implements the CQRS query handler pattern for retrieving payment line items by invoice line item ID.
    /// </remarks>
    public class GetPaymentLineItemsByInvoiceLineItemIdQueryHandler : IRequestHandler<GetPaymentLineItemsByInvoiceLineItemIdQuery, IEnumerable<PaymentLineItemListDto>>
    {
        private readonly IPaymentLineItemRepository _paymentLineItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPaymentLineItemsByInvoiceLineItemIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentLineItemsByInvoiceLineItemIdQueryHandler"/> class.
        /// </summary>
        /// <param name="paymentLineItemRepository">The payment line item repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetPaymentLineItemsByInvoiceLineItemIdQueryHandler(
            IPaymentLineItemRepository paymentLineItemRepository,
            IMapper mapper,
            ILogger<GetPaymentLineItemsByInvoiceLineItemIdQueryHandler> logger)
        {
            _paymentLineItemRepository = paymentLineItemRepository ?? throw new ArgumentNullException(nameof(paymentLineItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPaymentLineItemsByInvoiceLineItemIdQuery by retrieving payment line items for a specific invoice line item.
        /// </summary>
        /// <param name="request">The GetPaymentLineItemsByInvoiceLineItemIdQuery containing the invoice line item ID to retrieve payment line items for.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of PaymentLineItemListDto objects representing the payment line items for the specified invoice line item.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<IEnumerable<PaymentLineItemListDto>> Handle(GetPaymentLineItemsByInvoiceLineItemIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving payment line items for invoice line item with ID {InvoiceLineItemId}", request.InvoiceLineItemId);

                var paymentLineItems = await _paymentLineItemRepository.GetByInvoiceLineItemIdAsync(request.InvoiceLineItemId, cancellationToken);

                _logger.LogInformation("Successfully retrieved payment line items for invoice line item with ID {InvoiceLineItemId}", request.InvoiceLineItemId);

                return _mapper.Map<IEnumerable<PaymentLineItemListDto>>(paymentLineItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving payment line items for invoice line item with ID {InvoiceLineItemId}: {ErrorMessage}", request.InvoiceLineItemId, ex.Message);
                throw;
            }
        }
    }
}
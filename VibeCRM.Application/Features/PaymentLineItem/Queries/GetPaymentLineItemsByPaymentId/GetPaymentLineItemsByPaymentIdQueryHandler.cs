using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.PaymentLineItem.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.PaymentLineItem.Queries.GetPaymentLineItemsByPaymentId
{
    /// <summary>
    /// Handler for processing GetPaymentLineItemsByPaymentIdQuery requests.
    /// </summary>
    /// <remarks>
    /// Implements the CQRS query handler pattern for retrieving payment line items by payment ID.
    /// </remarks>
    public class GetPaymentLineItemsByPaymentIdQueryHandler : IRequestHandler<GetPaymentLineItemsByPaymentIdQuery, IEnumerable<PaymentLineItemListDto>>
    {
        private readonly IPaymentLineItemRepository _paymentLineItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPaymentLineItemsByPaymentIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentLineItemsByPaymentIdQueryHandler"/> class.
        /// </summary>
        /// <param name="paymentLineItemRepository">The payment line item repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetPaymentLineItemsByPaymentIdQueryHandler(
            IPaymentLineItemRepository paymentLineItemRepository,
            IMapper mapper,
            ILogger<GetPaymentLineItemsByPaymentIdQueryHandler> logger)
        {
            _paymentLineItemRepository = paymentLineItemRepository ?? throw new ArgumentNullException(nameof(paymentLineItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPaymentLineItemsByPaymentIdQuery by retrieving payment line items for a specific payment.
        /// </summary>
        /// <param name="request">The GetPaymentLineItemsByPaymentIdQuery containing the payment ID to retrieve line items for.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of PaymentLineItemListDto objects representing the payment line items for the specified payment.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<IEnumerable<PaymentLineItemListDto>> Handle(GetPaymentLineItemsByPaymentIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving payment line items for payment with ID {PaymentId}", request.PaymentId);

                var paymentLineItems = await _paymentLineItemRepository.GetByPaymentIdAsync(request.PaymentId, cancellationToken);

                _logger.LogInformation("Successfully retrieved payment line items for payment with ID {PaymentId}", request.PaymentId);

                return _mapper.Map<IEnumerable<PaymentLineItemListDto>>(paymentLineItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving payment line items for payment with ID {PaymentId}: {ErrorMessage}", request.PaymentId, ex.Message);
                throw;
            }
        }
    }
}
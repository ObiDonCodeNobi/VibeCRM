using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.PaymentLineItem.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.PaymentLineItem.Queries.GetPaymentLineItemById
{
    /// <summary>
    /// Handler for processing GetPaymentLineItemByIdQuery requests.
    /// </summary>
    /// <remarks>
    /// Implements the CQRS query handler pattern for retrieving a specific payment line item by ID.
    /// </remarks>
    public class GetPaymentLineItemByIdQueryHandler : IRequestHandler<GetPaymentLineItemByIdQuery, PaymentLineItemDetailsDto>
    {
        private readonly IPaymentLineItemRepository _paymentLineItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPaymentLineItemByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentLineItemByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="paymentLineItemRepository">The payment line item repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetPaymentLineItemByIdQueryHandler(
            IPaymentLineItemRepository paymentLineItemRepository,
            IMapper mapper,
            ILogger<GetPaymentLineItemByIdQueryHandler> logger)
        {
            _paymentLineItemRepository = paymentLineItemRepository ?? throw new ArgumentNullException(nameof(paymentLineItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPaymentLineItemByIdQuery by retrieving a specific payment line item from the database.
        /// </summary>
        /// <param name="request">The GetPaymentLineItemByIdQuery containing the payment line item ID to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A PaymentLineItemDetailsDto containing the requested payment line item details, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<PaymentLineItemDetailsDto> Handle(GetPaymentLineItemByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving payment line item with ID {PaymentLineItemId}", request.Id);

                var paymentLineItem = await _paymentLineItemRepository.GetByIdAsync(request.Id, cancellationToken);

                if (paymentLineItem == null)
                {
                    _logger.LogWarning("Payment line item with ID {PaymentLineItemId} not found", request.Id);
                    return new PaymentLineItemDetailsDto();
                }

                _logger.LogInformation("Successfully retrieved payment line item with ID {PaymentLineItemId}", request.Id);

                return _mapper.Map<PaymentLineItemDetailsDto>(paymentLineItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving payment line item with ID {PaymentLineItemId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
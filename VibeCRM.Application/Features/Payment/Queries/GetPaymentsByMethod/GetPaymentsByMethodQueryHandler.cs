using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Payment.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Payment.Queries.GetPaymentsByMethod
{
    /// <summary>
    /// Handler for processing GetPaymentsByMethodQuery requests.
    /// Implements the CQRS query handler pattern for retrieving payments with a specific payment method.
    /// </summary>
    public class GetPaymentsByMethodQueryHandler : IRequestHandler<GetPaymentsByMethodQuery, IEnumerable<PaymentListDto>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPaymentsByMethodQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentsByMethodQueryHandler"/> class.
        /// </summary>
        /// <param name="paymentRepository">The payment repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetPaymentsByMethodQueryHandler(
            IPaymentRepository paymentRepository,
            IMapper mapper,
            ILogger<GetPaymentsByMethodQueryHandler> logger)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPaymentsByMethodQuery by retrieving all payments with a specific payment method.
        /// </summary>
        /// <param name="request">The GetPaymentsByMethodQuery containing the payment method ID to retrieve payments for.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of PaymentListDto objects representing the payments with the specified method.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the payment method ID is empty.</exception>
        public async Task<IEnumerable<PaymentListDto>> Handle(GetPaymentsByMethodQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.PaymentMethodId == Guid.Empty) throw new ArgumentException("Payment method ID cannot be empty", nameof(request.PaymentMethodId));

            try
            {
                // Get payments by payment method ID (Active=1 filter is applied in the repository)
                var payments = await _paymentRepository.GetByPaymentMethodIdAsync(request.PaymentMethodId, cancellationToken);

                _logger.LogInformation("Retrieved payments with payment method ID: {PaymentMethodId}", request.PaymentMethodId);

                // Map entities to DTOs
                return _mapper.Map<IEnumerable<PaymentListDto>>(payments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving payments with payment method ID {PaymentMethodId}: {ErrorMessage}",
                    request.PaymentMethodId, ex.Message);
                throw;
            }
        }
    }
}
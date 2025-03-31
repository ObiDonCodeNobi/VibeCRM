using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Payment;

namespace VibeCRM.Application.Features.Payment.Queries.GetPaymentById
{
    /// <summary>
    /// Handler for processing GetPaymentByIdQuery requests.
    /// Implements the CQRS query handler pattern for retrieving a specific payment by its ID.
    /// </summary>
    public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, PaymentDetailsDto>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPaymentByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="paymentRepository">The payment repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetPaymentByIdQueryHandler(
            IPaymentRepository paymentRepository,
            IMapper mapper,
            ILogger<GetPaymentByIdQueryHandler> logger)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPaymentByIdQuery by retrieving a payment by its ID from the database.
        /// </summary>
        /// <param name="request">The query containing the ID of the payment to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A PaymentDetailsDto representing the requested payment, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the payment ID is empty.</exception>
        public async Task<PaymentDetailsDto> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Id == Guid.Empty) throw new ArgumentException("Payment ID cannot be empty", nameof(request.Id));

            try
            {
                // Get payment by ID (Active=1 filter is applied in the repository)
                var payment = await _paymentRepository.GetByIdAsync(request.Id, cancellationToken);

                if (payment == null)
                {
                    _logger.LogWarning("Payment with ID {PaymentId} not found", request.Id);
                    return new PaymentDetailsDto();
                }

                _logger.LogInformation("Retrieved payment with ID: {PaymentId}", payment.PaymentId);

                // Map entity to DTO
                return _mapper.Map<PaymentDetailsDto>(payment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving payment with ID {PaymentId}: {ErrorMessage}",
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}
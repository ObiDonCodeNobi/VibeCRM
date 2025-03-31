using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Payment;

namespace VibeCRM.Application.Features.Payment.Queries.GetPaymentsByStatus
{
    /// <summary>
    /// Handler for processing GetPaymentsByStatusQuery requests.
    /// Implements the CQRS query handler pattern for retrieving payments with a specific payment status.
    /// </summary>
    public class GetPaymentsByStatusQueryHandler : IRequestHandler<GetPaymentsByStatusQuery, IEnumerable<PaymentListDto>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPaymentsByStatusQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentsByStatusQueryHandler"/> class.
        /// </summary>
        /// <param name="paymentRepository">The payment repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetPaymentsByStatusQueryHandler(
            IPaymentRepository paymentRepository,
            IMapper mapper,
            ILogger<GetPaymentsByStatusQueryHandler> logger)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPaymentsByStatusQuery by retrieving payments by their status from the database.
        /// </summary>
        /// <param name="request">The query containing the status of the payments to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of PaymentListDto representing the requested payments, or an empty collection if none found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the payment status ID is empty.</exception>
        public async Task<IEnumerable<PaymentListDto>> Handle(GetPaymentsByStatusQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.PaymentStatusId == Guid.Empty) throw new ArgumentException("Payment status ID cannot be empty", nameof(request.PaymentStatusId));

            try
            {
                // Get payments by payment status ID (Active=1 filter is applied in the repository)
                var payments = await _paymentRepository.GetByPaymentStatusAsync(request.PaymentStatusId, cancellationToken);

                _logger.LogInformation("Retrieved payments with payment status ID: {PaymentStatusId}", request.PaymentStatusId);

                // Map entities to DTOs
                return _mapper.Map<IEnumerable<PaymentListDto>>(payments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving payments with payment status ID {PaymentStatusId}: {ErrorMessage}",
                    request.PaymentStatusId, ex.Message);
                throw;
            }
        }
    }
}
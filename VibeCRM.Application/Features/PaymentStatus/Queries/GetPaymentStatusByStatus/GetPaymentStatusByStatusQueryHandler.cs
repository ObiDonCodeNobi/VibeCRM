using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.PaymentStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PaymentStatus.Queries.GetPaymentStatusByStatus
{
    /// <summary>
    /// Handler for processing GetPaymentStatusByStatusQuery requests.
    /// Implements the CQRS query handler pattern for retrieving a specific payment status by its status name.
    /// </summary>
    public class GetPaymentStatusByStatusQueryHandler : IRequestHandler<GetPaymentStatusByStatusQuery, PaymentStatusDetailsDto>
    {
        private readonly IPaymentStatusRepository _paymentStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPaymentStatusByStatusQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentStatusByStatusQueryHandler"/> class.
        /// </summary>
        /// <param name="paymentStatusRepository">The payment status repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public GetPaymentStatusByStatusQueryHandler(
            IPaymentStatusRepository paymentStatusRepository,
            IMapper mapper,
            ILogger<GetPaymentStatusByStatusQueryHandler> logger)
        {
            _paymentStatusRepository = paymentStatusRepository ?? throw new ArgumentNullException(nameof(paymentStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPaymentStatusByStatusQuery by retrieving a specific payment status from the database.
        /// </summary>
        /// <param name="request">The GetPaymentStatusByStatusQuery containing the status name to search for.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A PaymentStatusDetailsDto representing the requested payment status, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<PaymentStatusDetailsDto> Handle(GetPaymentStatusByStatusQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving payment status with status name: {Status}", request.Status);

                // Get the payment status by status name
                var paymentStatus = await _paymentStatusRepository.GetByNameAsync(request.Status, cancellationToken);

                if (paymentStatus == null)
                {
                    _logger.LogWarning("Payment status with status name {Status} not found", request.Status);
                    return new PaymentStatusDetailsDto();
                }

                // Map to DTO
                var paymentStatusDto = _mapper.Map<PaymentStatusDetailsDto>(paymentStatus);

                // Note: In a real implementation, we would need to add a method to the repository
                // to get payment counts for this payment status. For now, we'll set it to 0.
                paymentStatusDto.PaymentCount = 0; // Default until repository supports payment count

                _logger.LogInformation("Successfully retrieved payment status with status name: {Status}", request.Status);

                return paymentStatusDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving payment status with status name {Status}: {ErrorMessage}",
                    request.Status, ex.Message);
                throw;
            }
        }
    }
}

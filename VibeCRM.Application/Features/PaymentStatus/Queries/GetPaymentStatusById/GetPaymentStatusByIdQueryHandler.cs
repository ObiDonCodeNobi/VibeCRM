using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.PaymentStatus;

namespace VibeCRM.Application.Features.PaymentStatus.Queries.GetPaymentStatusById
{
    /// <summary>
    /// Handler for processing GetPaymentStatusByIdQuery requests.
    /// Implements the CQRS query handler pattern for retrieving a specific payment status by its ID.
    /// </summary>
    public class GetPaymentStatusByIdQueryHandler : IRequestHandler<GetPaymentStatusByIdQuery, PaymentStatusDetailsDto>
    {
        private readonly IPaymentStatusRepository _paymentStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPaymentStatusByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentStatusByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="paymentStatusRepository">The payment status repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public GetPaymentStatusByIdQueryHandler(
            IPaymentStatusRepository paymentStatusRepository,
            IMapper mapper,
            ILogger<GetPaymentStatusByIdQueryHandler> logger)
        {
            _paymentStatusRepository = paymentStatusRepository ?? throw new ArgumentNullException(nameof(paymentStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPaymentStatusByIdQuery by retrieving a specific payment status from the database.
        /// </summary>
        /// <param name="request">The GetPaymentStatusByIdQuery containing the ID to search for.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A PaymentStatusDetailsDto representing the requested payment status, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<PaymentStatusDetailsDto> Handle(GetPaymentStatusByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving payment status with ID: {PaymentStatusId}", request.Id);

                // Get the payment status by ID
                var paymentStatus = await _paymentStatusRepository.GetByIdAsync(request.Id, cancellationToken);

                if (paymentStatus == null)
                {
                    _logger.LogWarning("Payment status with ID {PaymentStatusId} not found", request.Id);
                    return new PaymentStatusDetailsDto();
                }

                // Map to DTO
                var paymentStatusDto = _mapper.Map<PaymentStatusDetailsDto>(paymentStatus);

                // Note: In a real implementation, we would need to add a method to the repository
                // to get payment counts for this payment status. For now, we'll set it to 0.
                paymentStatusDto.PaymentCount = 0; // Default until repository supports payment count

                _logger.LogInformation("Successfully retrieved payment status with ID: {PaymentStatusId}", request.Id);

                return paymentStatusDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving payment status with ID {PaymentStatusId}: {ErrorMessage}",
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}
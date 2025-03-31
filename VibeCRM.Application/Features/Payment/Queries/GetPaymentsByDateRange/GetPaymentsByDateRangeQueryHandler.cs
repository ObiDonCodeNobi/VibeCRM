using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Payment;

namespace VibeCRM.Application.Features.Payment.Queries.GetPaymentsByDateRange
{
    /// <summary>
    /// Handler for processing GetPaymentsByDateRangeQuery requests.
    /// Implements the CQRS query handler pattern for retrieving payments within a specific date range.
    /// </summary>
    public class GetPaymentsByDateRangeQueryHandler : IRequestHandler<GetPaymentsByDateRangeQuery, IEnumerable<PaymentListDto>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPaymentsByDateRangeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentsByDateRangeQueryHandler"/> class.
        /// </summary>
        /// <param name="paymentRepository">The payment repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetPaymentsByDateRangeQueryHandler(
            IPaymentRepository paymentRepository,
            IMapper mapper,
            ILogger<GetPaymentsByDateRangeQueryHandler> logger)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPaymentsByDateRangeQuery by retrieving payments within the specified date range.
        /// </summary>
        /// <param name="request">The GetPaymentsByDateRangeQuery containing the date range to retrieve payments for.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of PaymentListDto objects representing the payments within the date range.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the date range is invalid.</exception>
        public async Task<IEnumerable<PaymentListDto>> Handle(GetPaymentsByDateRangeQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            // Validate date range
            if (request.EndDate < request.StartDate)
            {
                throw new ArgumentException("End date cannot be earlier than start date", nameof(request.EndDate));
            }

            try
            {
                // Get payments by date range (Active=1 filter is applied in the repository)
                var payments = await _paymentRepository.GetByDateRangeAsync(request.StartDate, request.EndDate, cancellationToken);

                _logger.LogInformation("Retrieved payments within date range: {StartDate} to {EndDate}",
                    request.StartDate.ToString("yyyy-MM-dd"), request.EndDate.ToString("yyyy-MM-dd"));

                // Map entities to DTOs
                return _mapper.Map<IEnumerable<PaymentListDto>>(payments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving payments within date range {StartDate} to {EndDate}: {ErrorMessage}",
                    request.StartDate.ToString("yyyy-MM-dd"), request.EndDate.ToString("yyyy-MM-dd"), ex.Message);
                throw;
            }
        }
    }
}
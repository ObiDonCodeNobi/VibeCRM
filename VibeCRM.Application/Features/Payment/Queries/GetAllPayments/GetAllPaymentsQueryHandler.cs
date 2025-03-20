using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Payment.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Payment.Queries.GetAllPayments
{
    /// <summary>
    /// Handler for processing GetAllPaymentsQuery requests.
    /// Implements the CQRS query handler pattern for retrieving all active payments in the system.
    /// </summary>
    public class GetAllPaymentsQueryHandler : IRequestHandler<GetAllPaymentsQuery, IEnumerable<PaymentListDto>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllPaymentsQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllPaymentsQueryHandler"/> class.
        /// </summary>
        /// <param name="paymentRepository">The payment repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetAllPaymentsQueryHandler(
            IPaymentRepository paymentRepository,
            IMapper mapper,
            ILogger<GetAllPaymentsQueryHandler> logger)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllPaymentsQuery by retrieving all active payments from the database.
        /// </summary>
        /// <param name="request">The GetAllPaymentsQuery request object.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of PaymentListDto objects representing all active payments.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<IEnumerable<PaymentListDto>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                // Get all active payments (Active=1 filter is applied in the repository)
                var payments = await _paymentRepository.GetAllAsync(cancellationToken);

                _logger.LogInformation("Retrieved all active payments");

                // Map entities to DTOs
                return _mapper.Map<IEnumerable<PaymentListDto>>(payments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all payments: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
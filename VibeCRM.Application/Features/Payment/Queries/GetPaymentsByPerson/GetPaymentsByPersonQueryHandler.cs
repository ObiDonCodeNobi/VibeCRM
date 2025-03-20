using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Payment.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Payment.Queries.GetPaymentsByPerson
{
    /// <summary>
    /// Handler for processing GetPaymentsByPersonQuery requests.
    /// Implements the CQRS query handler pattern for retrieving payments associated with a specific person.
    /// </summary>
    public class GetPaymentsByPersonQueryHandler : IRequestHandler<GetPaymentsByPersonQuery, IEnumerable<PaymentListDto>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPaymentsByPersonQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentsByPersonQueryHandler"/> class.
        /// </summary>
        /// <param name="paymentRepository">The payment repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetPaymentsByPersonQueryHandler(
            IPaymentRepository paymentRepository,
            IMapper mapper,
            ILogger<GetPaymentsByPersonQueryHandler> logger)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPaymentsByPersonQuery by retrieving all payments associated with a specific person.
        /// </summary>
        /// <param name="request">The GetPaymentsByPersonQuery containing the person ID to retrieve payments for.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of PaymentListDto objects representing the payments associated with the person.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the person ID is empty.</exception>
        public async Task<IEnumerable<PaymentListDto>> Handle(GetPaymentsByPersonQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.PersonId == Guid.Empty) throw new ArgumentException("Person ID cannot be empty", nameof(request.PersonId));

            try
            {
                // Get payments by person ID (Active=1 filter is applied in the repository)
                var payments = await _paymentRepository.GetByPersonIdAsync(request.PersonId, cancellationToken);

                _logger.LogInformation("Retrieved payments for person with ID: {PersonId}", request.PersonId);

                // Map entities to DTOs
                return _mapper.Map<IEnumerable<PaymentListDto>>(payments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving payments for person with ID {PersonId}: {ErrorMessage}",
                    request.PersonId, ex.Message);
                throw;
            }
        }
    }
}
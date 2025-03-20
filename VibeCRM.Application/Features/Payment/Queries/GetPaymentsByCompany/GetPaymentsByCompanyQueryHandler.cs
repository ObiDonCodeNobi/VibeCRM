using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Payment.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Payment.Queries.GetPaymentsByCompany
{
    /// <summary>
    /// Handler for processing GetPaymentsByCompanyQuery requests.
    /// Implements the CQRS query handler pattern for retrieving payments associated with a specific company.
    /// </summary>
    public class GetPaymentsByCompanyQueryHandler : IRequestHandler<GetPaymentsByCompanyQuery, IEnumerable<PaymentListDto>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPaymentsByCompanyQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentsByCompanyQueryHandler"/> class.
        /// </summary>
        /// <param name="paymentRepository">The payment repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetPaymentsByCompanyQueryHandler(
            IPaymentRepository paymentRepository,
            IMapper mapper,
            ILogger<GetPaymentsByCompanyQueryHandler> logger)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPaymentsByCompanyQuery by retrieving all payments associated with a specific company.
        /// </summary>
        /// <param name="request">The GetPaymentsByCompanyQuery containing the company ID to retrieve payments for.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of PaymentListDto objects representing the payments associated with the company.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the company ID is empty.</exception>
        public async Task<IEnumerable<PaymentListDto>> Handle(GetPaymentsByCompanyQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.CompanyId == Guid.Empty) throw new ArgumentException("Company ID cannot be empty", nameof(request.CompanyId));

            try
            {
                // Get payments by company ID (Active=1 filter is applied in the repository)
                var payments = await _paymentRepository.GetByCompanyIdAsync(request.CompanyId, cancellationToken);

                _logger.LogInformation("Retrieved payments for company with ID: {CompanyId}", request.CompanyId);

                // Map entities to DTOs
                return _mapper.Map<IEnumerable<PaymentListDto>>(payments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving payments for company with ID {CompanyId}: {ErrorMessage}",
                    request.CompanyId, ex.Message);
                throw;
            }
        }
    }
}
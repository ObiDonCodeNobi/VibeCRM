using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.PaymentStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PaymentStatus.Queries.GetAllPaymentStatuses
{
    /// <summary>
    /// Handler for processing GetAllPaymentStatusesQuery requests.
    /// Implements the CQRS query handler pattern for retrieving all payment statuses.
    /// </summary>
    public class GetAllPaymentStatusesQueryHandler : IRequestHandler<GetAllPaymentStatusesQuery, IEnumerable<PaymentStatusListDto>>
    {
        private readonly IPaymentStatusRepository _paymentStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllPaymentStatusesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllPaymentStatusesQueryHandler"/> class.
        /// </summary>
        /// <param name="paymentStatusRepository">The payment status repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public GetAllPaymentStatusesQueryHandler(
            IPaymentStatusRepository paymentStatusRepository,
            IMapper mapper,
            ILogger<GetAllPaymentStatusesQueryHandler> logger)
        {
            _paymentStatusRepository = paymentStatusRepository ?? throw new ArgumentNullException(nameof(paymentStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllPaymentStatusesQuery by retrieving all payment statuses from the database.
        /// </summary>
        /// <param name="request">The GetAllPaymentStatusesQuery containing filter options.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of PaymentStatusListDto objects representing all payment statuses.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<IEnumerable<PaymentStatusListDto>> Handle(GetAllPaymentStatusesQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving all payment statuses. Include inactive: {IncludeInactive}", request.IncludeInactive);

                // Get all payment statuses
                var paymentStatuses = await _paymentStatusRepository.GetAllAsync(cancellationToken);

                // Filter by active status if needed
                if (!request.IncludeInactive)
                {
                    paymentStatuses = paymentStatuses.Where(p => p.Active);
                }

                // Order by ordinal position
                paymentStatuses = paymentStatuses.OrderBy(p => p.OrdinalPosition);

                // Map to DTOs
                var paymentStatusDtos = _mapper.Map<IEnumerable<PaymentStatusListDto>>(paymentStatuses);

                // Note: In a real implementation, we would need to add a method to the repository
                // to get payment counts for each payment status. For now, we'll set them all to 0.
                foreach (var dto in paymentStatusDtos)
                {
                    dto.PaymentCount = 0; // Default until repository supports payment count
                }

                _logger.LogInformation("Successfully retrieved {Count} payment statuses", paymentStatusDtos.Count());

                return paymentStatusDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all payment statuses: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
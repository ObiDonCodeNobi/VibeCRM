using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.PaymentLineItem;

namespace VibeCRM.Application.Features.PaymentLineItem.Queries.GetAllPaymentLineItems
{
    /// <summary>
    /// Handler for processing GetAllPaymentLineItemsQuery requests.
    /// </summary>
    /// <remarks>
    /// Implements the CQRS query handler pattern for retrieving all active payment line items.
    /// </remarks>
    public class GetAllPaymentLineItemsQueryHandler : IRequestHandler<GetAllPaymentLineItemsQuery, IEnumerable<PaymentLineItemListDto>>
    {
        private readonly IPaymentLineItemRepository _paymentLineItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllPaymentLineItemsQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllPaymentLineItemsQueryHandler"/> class.
        /// </summary>
        /// <param name="paymentLineItemRepository">The payment line item repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetAllPaymentLineItemsQueryHandler(
            IPaymentLineItemRepository paymentLineItemRepository,
            IMapper mapper,
            ILogger<GetAllPaymentLineItemsQueryHandler> logger)
        {
            _paymentLineItemRepository = paymentLineItemRepository ?? throw new ArgumentNullException(nameof(paymentLineItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllPaymentLineItemsQuery by retrieving all active payment line items from the database.
        /// </summary>
        /// <param name="request">The GetAllPaymentLineItemsQuery request.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of PaymentLineItemListDto objects representing all active payment line items.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<IEnumerable<PaymentLineItemListDto>> Handle(GetAllPaymentLineItemsQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving all active payment line items");

                var paymentLineItems = await _paymentLineItemRepository.GetAllAsync(cancellationToken);

                _logger.LogInformation("Successfully retrieved all active payment line items");

                return _mapper.Map<IEnumerable<PaymentLineItemListDto>>(paymentLineItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all active payment line items: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
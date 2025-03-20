using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.PaymentMethod.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PaymentMethod.Queries.GetPaymentMethodById
{
    /// <summary>
    /// Handler for processing GetPaymentMethodByIdQuery requests
    /// </summary>
    public class GetPaymentMethodByIdQueryHandler : IRequestHandler<GetPaymentMethodByIdQuery, PaymentMethodDetailsDto>
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPaymentMethodByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the GetPaymentMethodByIdQueryHandler class
        /// </summary>
        /// <param name="paymentMethodRepository">The payment method repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetPaymentMethodByIdQueryHandler(
            IPaymentMethodRepository paymentMethodRepository,
            IMapper mapper,
            ILogger<GetPaymentMethodByIdQueryHandler> logger)
        {
            _paymentMethodRepository = paymentMethodRepository ?? throw new ArgumentNullException(nameof(paymentMethodRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPaymentMethodByIdQuery request
        /// </summary>
        /// <param name="request">The get payment method by ID query</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The payment method details DTO</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the payment method with the specified ID is not found</exception>
        /// <exception cref="ApplicationException">Thrown when an error occurs during retrieval of the payment method</exception>
        public async Task<PaymentMethodDetailsDto> Handle(GetPaymentMethodByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving payment method with ID: {Id}", request.Id);

                var paymentMethod = await _paymentMethodRepository.GetByIdAsync(request.Id, cancellationToken);
                if (paymentMethod == null)
                {
                    _logger.LogWarning("Payment method with ID: {Id} not found", request.Id);
                    throw new KeyNotFoundException($"Payment method with ID: {request.Id} not found");
                }

                var paymentMethodDto = _mapper.Map<PaymentMethodDetailsDto>(paymentMethod);

                _logger.LogInformation("Successfully retrieved payment method with ID: {Id}", request.Id);
                return paymentMethodDto;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving payment method with ID: {Id}", request.Id);
                throw new ApplicationException($"Error retrieving payment method with ID: {request.Id}", ex);
            }
        }
    }
}
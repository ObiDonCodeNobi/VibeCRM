using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.PaymentMethod.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PaymentMethod.Queries.GetAllPaymentMethods
{
    /// <summary>
    /// Handler for processing GetAllPaymentMethodsQuery requests
    /// </summary>
    public class GetAllPaymentMethodsQueryHandler : IRequestHandler<GetAllPaymentMethodsQuery, IEnumerable<PaymentMethodDto>>
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllPaymentMethodsQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the GetAllPaymentMethodsQueryHandler class
        /// </summary>
        /// <param name="paymentMethodRepository">The payment method repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetAllPaymentMethodsQueryHandler(
            IPaymentMethodRepository paymentMethodRepository,
            IMapper mapper,
            ILogger<GetAllPaymentMethodsQueryHandler> logger)
        {
            _paymentMethodRepository = paymentMethodRepository ?? throw new ArgumentNullException(nameof(paymentMethodRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllPaymentMethodsQuery request
        /// </summary>
        /// <param name="request">The get all payment methods query</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collection of payment method DTOs</returns>
        /// <exception cref="ApplicationException">Thrown when an error occurs during retrieval of payment methods</exception>
        public async Task<IEnumerable<PaymentMethodDto>> Handle(GetAllPaymentMethodsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving all payment methods");

                var paymentMethods = await _paymentMethodRepository.GetAllAsync(cancellationToken);
                var paymentMethodDtos = _mapper.Map<IEnumerable<PaymentMethodDto>>(paymentMethods);

                _logger.LogInformation("Successfully retrieved all payment methods");
                return paymentMethodDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all payment methods");
                throw new ApplicationException("Error retrieving all payment methods", ex);
            }
        }
    }
}
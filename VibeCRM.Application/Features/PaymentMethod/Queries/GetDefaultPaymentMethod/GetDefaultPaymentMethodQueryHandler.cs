using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.PaymentMethod.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PaymentMethod.Queries.GetDefaultPaymentMethod
{
    /// <summary>
    /// Handler for processing GetDefaultPaymentMethodQuery requests
    /// </summary>
    public class GetDefaultPaymentMethodQueryHandler : IRequestHandler<GetDefaultPaymentMethodQuery, PaymentMethodDto>
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDefaultPaymentMethodQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the GetDefaultPaymentMethodQueryHandler class
        /// </summary>
        /// <param name="paymentMethodRepository">The payment method repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetDefaultPaymentMethodQueryHandler(
            IPaymentMethodRepository paymentMethodRepository,
            IMapper mapper,
            ILogger<GetDefaultPaymentMethodQueryHandler> logger)
        {
            _paymentMethodRepository = paymentMethodRepository ?? throw new ArgumentNullException(nameof(paymentMethodRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetDefaultPaymentMethodQuery request
        /// </summary>
        /// <param name="request">The get default payment method query</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The default payment method DTO, or null if no payment methods exist</returns>
        /// <exception cref="ApplicationException">Thrown when an error occurs during retrieval of the default payment method</exception>
        public async Task<PaymentMethodDto> Handle(GetDefaultPaymentMethodQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving default payment method");

                var paymentMethod = await _paymentMethodRepository.GetDefaultAsync(cancellationToken);
                if (paymentMethod == null)
                {
                    _logger.LogWarning("No default payment method found");
                    return new PaymentMethodDto();
                }

                var paymentMethodDto = _mapper.Map<PaymentMethodDto>(paymentMethod);

                _logger.LogInformation("Successfully retrieved default payment method");
                return paymentMethodDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving default payment method");
                throw new ApplicationException("Error retrieving default payment method", ex);
            }
        }
    }
}

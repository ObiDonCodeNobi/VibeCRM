using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.PaymentMethod.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PaymentMethod.Queries.GetPaymentMethodByOrdinalPosition
{
    /// <summary>
    /// Handler for processing GetPaymentMethodByOrdinalPositionQuery requests
    /// </summary>
    public class GetPaymentMethodByOrdinalPositionQueryHandler : IRequestHandler<GetPaymentMethodByOrdinalPositionQuery, IEnumerable<PaymentMethodDto>>
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPaymentMethodByOrdinalPositionQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the GetPaymentMethodByOrdinalPositionQueryHandler class
        /// </summary>
        /// <param name="paymentMethodRepository">The payment method repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetPaymentMethodByOrdinalPositionQueryHandler(
            IPaymentMethodRepository paymentMethodRepository,
            IMapper mapper,
            ILogger<GetPaymentMethodByOrdinalPositionQueryHandler> logger)
        {
            _paymentMethodRepository = paymentMethodRepository ?? throw new ArgumentNullException(nameof(paymentMethodRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPaymentMethodByOrdinalPositionQuery request
        /// </summary>
        /// <param name="request">The get payment methods by ordinal position query</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collection of payment method DTOs ordered by ordinal position</returns>
        /// <exception cref="ApplicationException">Thrown when an error occurs during retrieval of payment methods</exception>
        public async Task<IEnumerable<PaymentMethodDto>> Handle(GetPaymentMethodByOrdinalPositionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving payment methods ordered by ordinal position");

                var paymentMethods = await _paymentMethodRepository.GetByOrdinalPositionAsync(cancellationToken);
                var paymentMethodDtos = _mapper.Map<IEnumerable<PaymentMethodDto>>(paymentMethods);

                _logger.LogInformation("Successfully retrieved payment methods ordered by ordinal position");
                return paymentMethodDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving payment methods ordered by ordinal position");
                throw new ApplicationException("Error retrieving payment methods ordered by ordinal position", ex);
            }
        }
    }
}

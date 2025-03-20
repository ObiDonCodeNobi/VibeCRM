using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.PaymentMethod.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PaymentMethod.Queries.GetPaymentMethodByName
{
    /// <summary>
    /// Handler for processing GetPaymentMethodByNameQuery requests
    /// </summary>
    public class GetPaymentMethodByNameQueryHandler : IRequestHandler<GetPaymentMethodByNameQuery, IEnumerable<PaymentMethodDto>>
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPaymentMethodByNameQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the GetPaymentMethodByNameQueryHandler class
        /// </summary>
        /// <param name="paymentMethodRepository">The payment method repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetPaymentMethodByNameQueryHandler(
            IPaymentMethodRepository paymentMethodRepository,
            IMapper mapper,
            ILogger<GetPaymentMethodByNameQueryHandler> logger)
        {
            _paymentMethodRepository = paymentMethodRepository ?? throw new ArgumentNullException(nameof(paymentMethodRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPaymentMethodByNameQuery request
        /// </summary>
        /// <param name="request">The get payment methods by name query</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collection of payment method DTOs matching the specified name</returns>
        /// <exception cref="ApplicationException">Thrown when an error occurs during retrieval of payment methods</exception>
        public async Task<IEnumerable<PaymentMethodDto>> Handle(GetPaymentMethodByNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving payment methods with name: {Name}", request.Name);

                var paymentMethods = await _paymentMethodRepository.GetByMethodAsync(request.Name, cancellationToken);
                var paymentMethodDtos = _mapper.Map<IEnumerable<PaymentMethodDto>>(paymentMethods);

                _logger.LogInformation("Successfully retrieved payment methods with name: {Name}", request.Name);
                return paymentMethodDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving payment methods with name: {Name}", request.Name);
                throw new ApplicationException($"Error retrieving payment methods with name: {request.Name}", ex);
            }
        }
    }
}

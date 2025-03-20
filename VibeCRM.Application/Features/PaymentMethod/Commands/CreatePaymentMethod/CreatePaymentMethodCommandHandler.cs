using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.PaymentMethod.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PaymentMethod.Commands.CreatePaymentMethod
{
    /// <summary>
    /// Handler for processing CreatePaymentMethodCommand requests
    /// </summary>
    public class CreatePaymentMethodCommandHandler : IRequestHandler<CreatePaymentMethodCommand, PaymentMethodDetailsDto>
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePaymentMethodCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the CreatePaymentMethodCommandHandler class
        /// </summary>
        /// <param name="paymentMethodRepository">The payment method repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public CreatePaymentMethodCommandHandler(
            IPaymentMethodRepository paymentMethodRepository,
            IMapper mapper,
            ILogger<CreatePaymentMethodCommandHandler> logger)
        {
            _paymentMethodRepository = paymentMethodRepository ?? throw new ArgumentNullException(nameof(paymentMethodRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreatePaymentMethodCommand request
        /// </summary>
        /// <param name="request">The create payment method command</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The created payment method details</returns>
        /// <exception cref="ApplicationException">Thrown when an error occurs during payment method creation</exception>
        public async Task<PaymentMethodDetailsDto> Handle(CreatePaymentMethodCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating new payment method: {Name}", request.Name);

                var entity = new Domain.Entities.TypeStatusEntities.PaymentMethod
                {
                    Name = request.Name,
                    Description = request.Description,
                    OrdinalPosition = request.OrdinalPosition,
                    CreatedBy = request.CreatedBy,
                    ModifiedBy = request.CreatedBy,
                    Active = true
                };

                var createdEntity = await _paymentMethodRepository.AddAsync(entity, cancellationToken);
                _logger.LogInformation("Successfully created payment method with ID: {Id}", createdEntity.Id);

                return _mapper.Map<PaymentMethodDetailsDto>(createdEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating payment method: {Name}", request.Name);
                throw new ApplicationException($"Error creating payment method: {ex.Message}", ex);
            }
        }
    }
}
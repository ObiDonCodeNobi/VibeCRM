using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Payment.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Payment.Commands.CreatePayment
{
    /// <summary>
    /// Handler for processing CreatePaymentCommand requests.
    /// Implements the CQRS command handler pattern for creating new payment records.
    /// </summary>
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, PaymentDto>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePaymentCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePaymentCommandHandler"/> class.
        /// </summary>
        /// <param name="paymentRepository">The payment repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public CreatePaymentCommandHandler(
            IPaymentRepository paymentRepository,
            IMapper mapper,
            ILogger<CreatePaymentCommandHandler> logger)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreatePaymentCommand by creating a new payment record in the database.
        /// </summary>
        /// <param name="request">The CreatePaymentCommand containing the payment data to create.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A PaymentDto representing the newly created payment.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when validation fails.</exception>
        public async Task<PaymentDto> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                // Map command to entity
                var paymentEntity = _mapper.Map<Domain.Entities.BusinessEntities.Payment>(request);

                // Set creation and modification dates
                paymentEntity.CreatedDate = DateTime.UtcNow;
                paymentEntity.ModifiedDate = DateTime.UtcNow;

                // Add payment to repository
                var createdPayment = await _paymentRepository.AddAsync(paymentEntity, cancellationToken);

                _logger.LogInformation("Payment created successfully with ID: {PaymentId}", createdPayment.PaymentId);

                // Map entity back to DTO and return
                return _mapper.Map<PaymentDto>(createdPayment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating payment: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
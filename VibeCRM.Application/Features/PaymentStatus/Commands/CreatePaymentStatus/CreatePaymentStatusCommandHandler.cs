using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.PaymentStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PaymentStatus.Commands.CreatePaymentStatus
{
    /// <summary>
    /// Handler for processing CreatePaymentStatusCommand requests.
    /// Implements the CQRS command handler pattern for creating payment status entities.
    /// </summary>
    public class CreatePaymentStatusCommandHandler : IRequestHandler<CreatePaymentStatusCommand, PaymentStatusDto>
    {
        private readonly IPaymentStatusRepository _paymentStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePaymentStatusCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePaymentStatusCommandHandler"/> class.
        /// </summary>
        /// <param name="paymentStatusRepository">The payment status repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public CreatePaymentStatusCommandHandler(
            IPaymentStatusRepository paymentStatusRepository,
            IMapper mapper,
            ILogger<CreatePaymentStatusCommandHandler> logger)
        {
            _paymentStatusRepository = paymentStatusRepository ?? throw new ArgumentNullException(nameof(paymentStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreatePaymentStatusCommand by creating a new payment status entity in the database.
        /// </summary>
        /// <param name="request">The CreatePaymentStatusCommand containing the payment status data to create.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A PaymentStatusDto representing the newly created payment status.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<PaymentStatusDto> Handle(CreatePaymentStatusCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Creating new payment status with ID: {PaymentStatusId}", request.Id);

                // Map the command to an entity
                var paymentStatusEntity = _mapper.Map<Domain.Entities.TypeStatusEntities.PaymentStatus>(request);

                // Set audit fields
                paymentStatusEntity.CreatedDate = DateTime.UtcNow;
                paymentStatusEntity.ModifiedDate = DateTime.UtcNow;
                paymentStatusEntity.Active = true;

                // Save to database
                var createdPaymentStatus = await _paymentStatusRepository.AddAsync(paymentStatusEntity, cancellationToken);

                _logger.LogInformation("Successfully created payment status with ID: {PaymentStatusId}", createdPaymentStatus.PaymentStatusId);

                // Map the entity back to a DTO for the response
                return _mapper.Map<PaymentStatusDto>(createdPaymentStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating payment status with ID {PaymentStatusId}: {ErrorMessage}",
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}
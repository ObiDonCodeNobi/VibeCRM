using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Payment.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Payment.Commands.UpdatePayment
{
    /// <summary>
    /// Handler for processing UpdatePaymentCommand requests.
    /// Implements the CQRS command handler pattern for updating existing payment records.
    /// </summary>
    public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, PaymentDto>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePaymentCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePaymentCommandHandler"/> class.
        /// </summary>
        /// <param name="paymentRepository">The payment repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public UpdatePaymentCommandHandler(
            IPaymentRepository paymentRepository,
            IMapper mapper,
            ILogger<UpdatePaymentCommandHandler> logger)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdatePaymentCommand by updating an existing payment record in the database.
        /// </summary>
        /// <param name="request">The UpdatePaymentCommand containing the payment data to update.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A PaymentDto representing the updated payment.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when validation fails or the payment is not found.</exception>
        public async Task<PaymentDto> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                // Get the existing payment
                var existingPayment = await _paymentRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingPayment == null)
                {
                    _logger.LogWarning("Payment with ID {PaymentId} not found for update", request.Id);
                    throw new ArgumentException($"Payment with ID {request.Id} not found", nameof(request.Id));
                }

                // Map command to entity, preserving created by and created date
                var paymentToUpdate = _mapper.Map<UpdatePaymentCommand, Domain.Entities.BusinessEntities.Payment>(request);
                paymentToUpdate.CreatedBy = existingPayment.CreatedBy;
                paymentToUpdate.CreatedDate = existingPayment.CreatedDate;
                paymentToUpdate.ModifiedDate = DateTime.UtcNow;
                paymentToUpdate.Active = existingPayment.Active;

                // Update payment in repository
                var updatedPayment = await _paymentRepository.UpdateAsync(paymentToUpdate, cancellationToken);

                _logger.LogInformation("Payment updated successfully with ID: {PaymentId}", updatedPayment.PaymentId);

                // Map entity back to DTO and return
                return _mapper.Map<PaymentDto>(updatedPayment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating payment with ID {PaymentId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
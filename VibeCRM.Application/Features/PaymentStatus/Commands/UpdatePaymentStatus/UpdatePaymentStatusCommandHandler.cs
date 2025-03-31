using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.PaymentStatus;

namespace VibeCRM.Application.Features.PaymentStatus.Commands.UpdatePaymentStatus
{
    /// <summary>
    /// Handler for processing UpdatePaymentStatusCommand requests.
    /// Implements the CQRS command handler pattern for updating payment status entities.
    /// </summary>
    public class UpdatePaymentStatusCommandHandler : IRequestHandler<UpdatePaymentStatusCommand, PaymentStatusDto>
    {
        private readonly IPaymentStatusRepository _paymentStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePaymentStatusCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePaymentStatusCommandHandler"/> class.
        /// </summary>
        /// <param name="paymentStatusRepository">The payment status repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public UpdatePaymentStatusCommandHandler(
            IPaymentStatusRepository paymentStatusRepository,
            IMapper mapper,
            ILogger<UpdatePaymentStatusCommandHandler> logger)
        {
            _paymentStatusRepository = paymentStatusRepository ?? throw new ArgumentNullException(nameof(paymentStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdatePaymentStatusCommand by updating an existing payment status entity in the database.
        /// </summary>
        /// <param name="request">The UpdatePaymentStatusCommand containing the payment status data to update.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A PaymentStatusDto representing the updated payment status.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<PaymentStatusDto> Handle(UpdatePaymentStatusCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Updating payment status with ID: {PaymentStatusId}", request.Id);

                // Get the existing entity
                var existingPaymentStatus = await _paymentStatusRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingPaymentStatus == null)
                {
                    _logger.LogWarning("Payment status with ID {PaymentStatusId} not found", request.Id);
                    throw new KeyNotFoundException($"Payment status with ID {request.Id} not found");
                }

                // Map the command to the existing entity
                _mapper.Map(request, existingPaymentStatus);

                // Update audit fields
                existingPaymentStatus.ModifiedDate = DateTime.UtcNow;
                existingPaymentStatus.ModifiedBy = request.ModifiedBy;

                // Save to database
                var updatedPaymentStatus = await _paymentStatusRepository.UpdateAsync(existingPaymentStatus, cancellationToken);

                _logger.LogInformation("Successfully updated payment status with ID: {PaymentStatusId}", updatedPaymentStatus.PaymentStatusId);

                // Map the entity back to a DTO for the response
                return _mapper.Map<PaymentStatusDto>(updatedPaymentStatus);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating payment status with ID {PaymentStatusId}: {ErrorMessage}",
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}
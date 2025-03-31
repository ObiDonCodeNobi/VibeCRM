using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.PaymentLineItem;

namespace VibeCRM.Application.Features.PaymentLineItem.Commands.UpdatePaymentLineItem
{
    /// <summary>
    /// Handler for processing UpdatePaymentLineItemCommand requests.
    /// </summary>
    /// <remarks>
    /// Implements the CQRS command handler pattern for updating payment line item records.
    /// </remarks>
    public class UpdatePaymentLineItemCommandHandler : IRequestHandler<UpdatePaymentLineItemCommand, PaymentLineItemDetailsDto>
    {
        private readonly IPaymentLineItemRepository _paymentLineItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePaymentLineItemCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePaymentLineItemCommandHandler"/> class.
        /// </summary>
        /// <param name="paymentLineItemRepository">The payment line item repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public UpdatePaymentLineItemCommandHandler(
            IPaymentLineItemRepository paymentLineItemRepository,
            IMapper mapper,
            ILogger<UpdatePaymentLineItemCommandHandler> logger)
        {
            _paymentLineItemRepository = paymentLineItemRepository ?? throw new ArgumentNullException(nameof(paymentLineItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdatePaymentLineItemCommand by updating an existing payment line item in the database.
        /// </summary>
        /// <param name="request">The UpdatePaymentLineItemCommand containing the payment line item details to update.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A PaymentLineItemDetailsDto containing the updated payment line item details.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when validation fails or the payment line item is not found.</exception>
        public async Task<PaymentLineItemDetailsDto> Handle(UpdatePaymentLineItemCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                // Get the existing payment line item
                var existingPaymentLineItem = await _paymentLineItemRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingPaymentLineItem == null)
                {
                    _logger.LogWarning("Payment line item with ID {PaymentLineItemId} not found for update", request.Id);
                    throw new ArgumentException($"Payment line item with ID {request.Id} not found", nameof(request.Id));
                }

                _logger.LogInformation("Updating payment line item with ID {PaymentLineItemId}", request.Id);

                // Map command to entity, preserving original values for fields not in the command
                _mapper.Map(request, existingPaymentLineItem);

                // Set modified by and update modified date
                existingPaymentLineItem.ModifiedBy = Guid.TryParse(request.ModifiedBy, out var modifiedByGuid) ? modifiedByGuid : Guid.Empty;
                existingPaymentLineItem.ModifiedDate = DateTime.UtcNow;

                // Update in repository
                var updatedPaymentLineItem = await _paymentLineItemRepository.UpdateAsync(existingPaymentLineItem, cancellationToken);

                _logger.LogInformation("Successfully updated payment line item with ID {PaymentLineItemId}", updatedPaymentLineItem.PaymentLineItemId);

                // Map entity to DTO
                return _mapper.Map<PaymentLineItemDetailsDto>(updatedPaymentLineItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating payment line item with ID {PaymentLineItemId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
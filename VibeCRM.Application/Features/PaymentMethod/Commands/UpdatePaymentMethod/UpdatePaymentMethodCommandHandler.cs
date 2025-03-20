using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PaymentMethod.Commands.UpdatePaymentMethod
{
    /// <summary>
    /// Handler for processing UpdatePaymentMethodCommand requests
    /// </summary>
    public class UpdatePaymentMethodCommandHandler : IRequestHandler<UpdatePaymentMethodCommand, bool>
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePaymentMethodCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the UpdatePaymentMethodCommandHandler class
        /// </summary>
        /// <param name="paymentMethodRepository">The payment method repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public UpdatePaymentMethodCommandHandler(
            IPaymentMethodRepository paymentMethodRepository,
            IMapper mapper,
            ILogger<UpdatePaymentMethodCommandHandler> logger)
        {
            _paymentMethodRepository = paymentMethodRepository ?? throw new ArgumentNullException(nameof(paymentMethodRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdatePaymentMethodCommand request
        /// </summary>
        /// <param name="request">The update payment method command</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>True if the update was successful, otherwise false</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the payment method with the specified ID is not found</exception>
        /// <exception cref="ApplicationException">Thrown when an error occurs during payment method update</exception>
        public async Task<bool> Handle(UpdatePaymentMethodCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating payment method with ID: {Id}", request.Id);

                var existingEntity = await _paymentMethodRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingEntity == null)
                {
                    _logger.LogWarning("Payment method with ID: {Id} not found", request.Id);
                    throw new KeyNotFoundException($"Payment method with ID: {request.Id} not found");
                }

                // Update the entity properties
                existingEntity.Name = request.Name;
                existingEntity.Description = request.Description;
                existingEntity.OrdinalPosition = request.OrdinalPosition;
                existingEntity.ModifiedBy = request.ModifiedBy;

                await _paymentMethodRepository.UpdateAsync(existingEntity, cancellationToken);
                _logger.LogInformation("Successfully updated payment method with ID: {Id}", request.Id);

                return true;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating payment method with ID: {Id}", request.Id);
                throw new ApplicationException($"Error updating payment method with ID: {request.Id}", ex);
            }
        }
    }
}
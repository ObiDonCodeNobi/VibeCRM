using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PaymentMethod.Commands.DeletePaymentMethod
{
    /// <summary>
    /// Handler for processing DeletePaymentMethodCommand requests
    /// </summary>
    public class DeletePaymentMethodCommandHandler : IRequestHandler<DeletePaymentMethodCommand, bool>
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeletePaymentMethodCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the DeletePaymentMethodCommandHandler class
        /// </summary>
        /// <param name="paymentMethodRepository">The payment method repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public DeletePaymentMethodCommandHandler(
            IPaymentMethodRepository paymentMethodRepository,
            IMapper mapper,
            ILogger<DeletePaymentMethodCommandHandler> logger)
        {
            _paymentMethodRepository = paymentMethodRepository ?? throw new ArgumentNullException(nameof(paymentMethodRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeletePaymentMethodCommand request
        /// </summary>
        /// <param name="request">The delete payment method command</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>True if the deletion was successful, otherwise false</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the payment method with the specified ID is not found</exception>
        /// <exception cref="ApplicationException">Thrown when an error occurs during payment method deletion</exception>
        public async Task<bool> Handle(DeletePaymentMethodCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Deleting payment method with ID: {Id}", request.Id);

                var existingEntity = await _paymentMethodRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingEntity == null)
                {
                    _logger.LogWarning("Payment method with ID: {Id} not found", request.Id);
                    throw new KeyNotFoundException($"Payment method with ID: {request.Id} not found");
                }

                // Set the ModifiedBy before deleting
                existingEntity.ModifiedBy = request.ModifiedBy;
                
                // This will perform a soft delete by setting Active = false
                await _paymentMethodRepository.DeleteAsync(request.Id, cancellationToken);
                
                _logger.LogInformation("Successfully deleted payment method with ID: {Id}", request.Id);
                return true;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting payment method with ID: {Id}", request.Id);
                throw new ApplicationException($"Error deleting payment method with ID: {request.Id}", ex);
            }
        }
    }
}

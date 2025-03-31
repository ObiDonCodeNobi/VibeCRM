using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.PaymentLineItem;

namespace VibeCRM.Application.Features.PaymentLineItem.Commands.CreatePaymentLineItem
{
    /// <summary>
    /// Handler for processing CreatePaymentLineItemCommand requests.
    /// </summary>
    /// <remarks>
    /// Implements the CQRS command handler pattern for creating payment line item records.
    /// </remarks>
    public class CreatePaymentLineItemCommandHandler : IRequestHandler<CreatePaymentLineItemCommand, PaymentLineItemDetailsDto>
    {
        private readonly IPaymentLineItemRepository _paymentLineItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePaymentLineItemCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePaymentLineItemCommandHandler"/> class.
        /// </summary>
        /// <param name="paymentLineItemRepository">The payment line item repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public CreatePaymentLineItemCommandHandler(
            IPaymentLineItemRepository paymentLineItemRepository,
            IMapper mapper,
            ILogger<CreatePaymentLineItemCommandHandler> logger)
        {
            _paymentLineItemRepository = paymentLineItemRepository ?? throw new ArgumentNullException(nameof(paymentLineItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreatePaymentLineItemCommand by creating a new payment line item in the database.
        /// </summary>
        /// <param name="request">The CreatePaymentLineItemCommand containing the payment line item details to create.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A PaymentLineItemDetailsDto containing the created payment line item details.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<PaymentLineItemDetailsDto> Handle(CreatePaymentLineItemCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Creating new payment line item with ID {PaymentLineItemId}", request.Id);

                // Map command to entity
                var paymentLineItemEntity = _mapper.Map<Domain.Entities.BusinessEntities.PaymentLineItem>(request);

                // Set audit fields
                paymentLineItemEntity.CreatedBy = Guid.TryParse(request.CreatedBy, out var createdByGuid) ? createdByGuid : Guid.Empty;
                paymentLineItemEntity.ModifiedBy = Guid.TryParse(request.CreatedBy, out var modifiedByGuid) ? modifiedByGuid : Guid.Empty;
                paymentLineItemEntity.PaymentLineItemId = Guid.NewGuid();
                paymentLineItemEntity.CreatedDate = DateTime.UtcNow;
                paymentLineItemEntity.ModifiedDate = DateTime.UtcNow;

                // Add to repository
                var createdPaymentLineItem = await _paymentLineItemRepository.AddAsync(paymentLineItemEntity, cancellationToken);

                _logger.LogInformation("Successfully created payment line item with ID {PaymentLineItemId}", createdPaymentLineItem.PaymentLineItemId);

                // Map entity to DTO
                return _mapper.Map<PaymentLineItemDetailsDto>(createdPaymentLineItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating payment line item with ID {PaymentLineItemId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
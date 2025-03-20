using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.QuoteLineItem.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.QuoteLineItem.Commands.UpdateQuoteLineItem
{
    /// <summary>
    /// Handler for processing the UpdateQuoteLineItemCommand
    /// </summary>
    public class UpdateQuoteLineItemCommandHandler : IRequestHandler<UpdateQuoteLineItemCommand, QuoteLineItemDetailsDto>
    {
        private readonly IQuoteLineItemRepository _quoteLineItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateQuoteLineItemCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateQuoteLineItemCommandHandler"/> class
        /// </summary>
        /// <param name="quoteLineItemRepository">The quote line item repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public UpdateQuoteLineItemCommandHandler(
            IQuoteLineItemRepository quoteLineItemRepository,
            IMapper mapper,
            ILogger<UpdateQuoteLineItemCommandHandler> logger)
        {
            _quoteLineItemRepository = quoteLineItemRepository ?? throw new ArgumentNullException(nameof(quoteLineItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateQuoteLineItemCommand
        /// </summary>
        /// <param name="request">The command to update a quote line item</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The updated quote line item details</returns>
        /// <exception cref="InvalidOperationException">Thrown when the quote line item does not exist</exception>
        public async Task<QuoteLineItemDetailsDto> Handle(UpdateQuoteLineItemCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating quote line item with ID {QuoteLineItemId}", request.Id);

            try
            {
                // Get the existing quote line item
                var existingQuoteLineItem = await _quoteLineItemRepository.GetByIdAsync(request.Id, cancellationToken);

                if (existingQuoteLineItem == null || !existingQuoteLineItem.Active)
                {
                    _logger.LogWarning("Quote line item with ID {QuoteLineItemId} not found or inactive", request.Id);
                    throw new InvalidOperationException($"Quote line item with ID {request.Id} not found or inactive");
                }

                // Map the updated values to the existing entity
                _mapper.Map(request, existingQuoteLineItem);

                // Ensure the ID is preserved and update audit fields
                existingQuoteLineItem.QuoteLineItemId = request.Id;
                existingQuoteLineItem.ModifiedBy = request.ModifiedBy;
                existingQuoteLineItem.ModifiedDate = DateTime.UtcNow;

                // Update the entity in the repository
                var updatedQuoteLineItem = await _quoteLineItemRepository.UpdateAsync(existingQuoteLineItem, cancellationToken);

                _logger.LogInformation("Successfully updated quote line item with ID {QuoteLineItemId}", updatedQuoteLineItem.QuoteLineItemId);

                return _mapper.Map<QuoteLineItemDetailsDto>(updatedQuoteLineItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating quote line item with ID {QuoteLineItemId}", request.Id);
                throw;
            }
        }
    }
}
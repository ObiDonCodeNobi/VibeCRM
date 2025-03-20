using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.QuoteLineItem.Commands.DeleteQuoteLineItem
{
    /// <summary>
    /// Handler for processing the DeleteQuoteLineItemCommand
    /// </summary>
    public class DeleteQuoteLineItemCommandHandler : IRequestHandler<DeleteQuoteLineItemCommand, bool>
    {
        private readonly IQuoteLineItemRepository _quoteLineItemRepository;
        private readonly ILogger<DeleteQuoteLineItemCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteQuoteLineItemCommandHandler"/> class
        /// </summary>
        /// <param name="quoteLineItemRepository">The quote line item repository</param>
        /// <param name="logger">The logger</param>
        public DeleteQuoteLineItemCommandHandler(
            IQuoteLineItemRepository quoteLineItemRepository,
            ILogger<DeleteQuoteLineItemCommandHandler> logger)
        {
            _quoteLineItemRepository = quoteLineItemRepository ?? throw new ArgumentNullException(nameof(quoteLineItemRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteQuoteLineItemCommand
        /// </summary>
        /// <param name="request">The command to delete a quote line item</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>True if the deletion was successful, otherwise false</returns>
        public async Task<bool> Handle(DeleteQuoteLineItemCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Soft deleting quote line item with ID {QuoteLineItemId}", request.Id);

            try
            {
                // Get the existing quote line item
                var existingQuoteLineItem = await _quoteLineItemRepository.GetByIdAsync(request.Id, cancellationToken);

                if (existingQuoteLineItem == null || !existingQuoteLineItem.Active)
                {
                    _logger.LogWarning("Quote line item with ID {QuoteLineItemId} not found or already inactive", request.Id);
                    return false;
                }

                // Update the ModifiedBy property before soft delete
                existingQuoteLineItem.ModifiedBy = request.ModifiedBy;
                existingQuoteLineItem.ModifiedDate = DateTime.UtcNow;

                // Perform soft delete (sets Active = false)
                var result = await _quoteLineItemRepository.DeleteAsync(request.Id, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Successfully soft deleted quote line item with ID {QuoteLineItemId}", request.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to soft delete quote line item with ID {QuoteLineItemId}", request.Id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error soft deleting quote line item with ID {QuoteLineItemId}", request.Id);
                throw;
            }
        }
    }
}
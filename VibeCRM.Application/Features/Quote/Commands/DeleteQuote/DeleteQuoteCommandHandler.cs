using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Quote.Commands.DeleteQuote
{
    /// <summary>
    /// Handler for processing DeleteQuoteCommand requests.
    /// Implements the CQRS command handler pattern for soft-deleting quote entities.
    /// </summary>
    public class DeleteQuoteCommandHandler : IRequestHandler<DeleteQuoteCommand, bool>
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly ILogger<DeleteQuoteCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteQuoteCommandHandler"/> class.
        /// </summary>
        /// <param name="quoteRepository">The quote repository for database operations.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public DeleteQuoteCommandHandler(
            IQuoteRepository quoteRepository,
            ILogger<DeleteQuoteCommandHandler> logger)
        {
            _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteQuoteCommand by soft-deleting an existing quote entity in the database.
        /// </summary>
        /// <param name="request">The DeleteQuoteCommand containing the quote ID to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the quote to delete is not found.</exception>
        public async Task<bool> Handle(DeleteQuoteCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            // Retrieve the existing quote
            var existingQuote = await _quoteRepository.GetByIdAsync(request.Id, cancellationToken);
            if (existingQuote == null || !existingQuote.Active)
            {
                _logger.LogError("Quote not found for deletion with ID: {QuoteId}", request.Id);
                throw new InvalidOperationException($"Quote with ID {request.Id} not found or already deleted.");
            }

            try
            {
                // Update the modified by information before deletion
                existingQuote.ModifiedBy = request.ModifiedBy;
                existingQuote.ModifiedDate = DateTime.UtcNow;

                // Perform the soft delete operation
                await _quoteRepository.DeleteAsync(existingQuote.QuoteId, cancellationToken);
                _logger.LogInformation("Soft deleted quote with ID: {QuoteId}", request.Id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting quote with ID: {QuoteId}", request.Id);
                throw;
            }
        }
    }
}
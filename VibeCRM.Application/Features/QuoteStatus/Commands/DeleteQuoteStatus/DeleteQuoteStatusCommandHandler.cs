using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.QuoteStatus.Commands.DeleteQuoteStatus
{
    /// <summary>
    /// Handler for the DeleteQuoteStatusCommand.
    /// Soft-deletes an existing quote status in the database.
    /// </summary>
    public class DeleteQuoteStatusCommandHandler : IRequestHandler<DeleteQuoteStatusCommand, bool>
    {
        private readonly IQuoteStatusRepository _quoteStatusRepository;
        private readonly ILogger<DeleteQuoteStatusCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteQuoteStatusCommandHandler"/> class.
        /// </summary>
        /// <param name="quoteStatusRepository">The quote status repository.</param>
        /// <param name="logger">The logger.</param>
        public DeleteQuoteStatusCommandHandler(
            IQuoteStatusRepository quoteStatusRepository,
            ILogger<DeleteQuoteStatusCommandHandler> logger)
        {
            _quoteStatusRepository = quoteStatusRepository ?? throw new ArgumentNullException(nameof(quoteStatusRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteQuoteStatusCommand by soft-deleting an existing quote status in the database.
        /// Sets the Active property to false rather than physically removing the record.
        /// </summary>
        /// <param name="request">The command request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if the quote status was successfully deleted, otherwise false.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the quote status to delete is not found.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the deletion process.</exception>
        public async Task<bool> Handle(DeleteQuoteStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Soft-deleting quote status with ID: {QuoteStatusId}", request.Id);

                // Check if quote status exists
                var existingQuoteStatus = await _quoteStatusRepository.GetByIdAsync(request.Id, cancellationToken);

                if (existingQuoteStatus == null)
                {
                    _logger.LogError("Quote status with ID {QuoteStatusId} not found", request.Id);
                    throw new KeyNotFoundException($"Quote status with ID {request.Id} not found");
                }

                // Delete quote status (soft delete) - pass the ID, not the entity
                var result = await _quoteStatusRepository.DeleteAsync(request.Id, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Successfully soft-deleted quote status with ID: {QuoteStatusId}", request.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to soft-delete quote status with ID: {QuoteStatusId}", request.Id);
                }

                return result;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error soft-deleting quote status: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
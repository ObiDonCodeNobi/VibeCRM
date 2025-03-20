using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Quote.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Quote.Commands.UpdateQuote
{
    /// <summary>
    /// Handler for processing UpdateQuoteCommand requests.
    /// Implements the CQRS command handler pattern for updating quote entities.
    /// </summary>
    public class UpdateQuoteCommandHandler : IRequestHandler<UpdateQuoteCommand, QuoteDetailsDto>
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateQuoteCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateQuoteCommandHandler"/> class.
        /// </summary>
        /// <param name="quoteRepository">The quote repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public UpdateQuoteCommandHandler(
            IQuoteRepository quoteRepository,
            IMapper mapper,
            ILogger<UpdateQuoteCommandHandler> logger)
        {
            _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateQuoteCommand by updating an existing quote entity in the database.
        /// </summary>
        /// <param name="request">The UpdateQuoteCommand containing the quote details to update.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A QuoteDetailsDto representing the updated quote with all related entities.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the quote to update is not found.</exception>
        public async Task<QuoteDetailsDto> Handle(UpdateQuoteCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            // Retrieve the existing quote
            var existingQuote = await _quoteRepository.GetByIdAsync(request.Id, cancellationToken);
            if (existingQuote == null || !existingQuote.Active)
            {
                _logger.LogError("Quote not found for update with ID: {QuoteId}", request.Id);
                throw new InvalidOperationException($"Quote with ID {request.Id} not found or inactive.");
            }

            try
            {
                // Update the quote properties
                existingQuote.Number = request.Number;
                existingQuote.QuoteStatusId = request.QuoteStatusId;
                existingQuote.ModifiedBy = request.ModifiedBy;
                existingQuote.ModifiedDate = DateTime.UtcNow;

                // Update the quote in the repository
                var updatedQuote = await _quoteRepository.UpdateAsync(existingQuote, cancellationToken);

                // Load related entities for the updated quote
                if (updatedQuote.QuoteStatusId.HasValue)
                {
                    await _quoteRepository.LoadQuoteStatusAsync(updatedQuote, cancellationToken);
                }
                
                await _quoteRepository.LoadLineItemsAsync(updatedQuote, cancellationToken);
                await _quoteRepository.LoadSalesOrdersAsync(updatedQuote, cancellationToken);

                _logger.LogInformation("Updated quote with ID: {QuoteId}", updatedQuote.QuoteId);

                // Map the updated entity to DTO
                return _mapper.Map<QuoteDetailsDto>(updatedQuote);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating quote with ID: {QuoteId}", request.Id);
                throw;
            }
        }
    }
}
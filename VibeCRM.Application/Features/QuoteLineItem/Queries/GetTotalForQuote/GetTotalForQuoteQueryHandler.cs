using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.QuoteLineItem.Queries.GetTotalForQuote
{
    /// <summary>
    /// Handler for processing the GetTotalForQuoteQuery
    /// </summary>
    public class GetTotalForQuoteQueryHandler : IRequestHandler<GetTotalForQuoteQuery, decimal>
    {
        private readonly IQuoteLineItemRepository _quoteLineItemRepository;
        private readonly ILogger<GetTotalForQuoteQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTotalForQuoteQueryHandler"/> class
        /// </summary>
        /// <param name="quoteLineItemRepository">The quote line item repository</param>
        /// <param name="logger">The logger</param>
        public GetTotalForQuoteQueryHandler(
            IQuoteLineItemRepository quoteLineItemRepository,
            ILogger<GetTotalForQuoteQueryHandler> logger)
        {
            _quoteLineItemRepository = quoteLineItemRepository ?? throw new ArgumentNullException(nameof(quoteLineItemRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetTotalForQuoteQuery
        /// </summary>
        /// <param name="request">The query to calculate the total amount for a quote</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The total amount for the specified quote</returns>
        public async Task<decimal> Handle(GetTotalForQuoteQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Calculating total amount for quote with ID {QuoteId}", request.QuoteId);

            try
            {
                var total = await _quoteLineItemRepository.GetTotalForQuoteAsync(request.QuoteId, cancellationToken);

                _logger.LogInformation("Successfully calculated total amount for quote with ID {QuoteId}: {TotalAmount}",
                    request.QuoteId, total);

                return total;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating total amount for quote with ID {QuoteId}", request.QuoteId);
                throw;
            }
        }
    }
}
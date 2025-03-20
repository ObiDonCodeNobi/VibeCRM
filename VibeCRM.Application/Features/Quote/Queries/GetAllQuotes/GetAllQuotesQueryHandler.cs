using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Quote.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Quote.Queries.GetAllQuotes
{
    /// <summary>
    /// Handler for processing GetAllQuotesQuery requests.
    /// Implements the CQRS query handler pattern for retrieving all active quotes.
    /// </summary>
    public class GetAllQuotesQueryHandler : IRequestHandler<GetAllQuotesQuery, IEnumerable<QuoteListDto>>
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllQuotesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllQuotesQueryHandler"/> class.
        /// </summary>
        /// <param name="quoteRepository">The quote repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetAllQuotesQueryHandler(
            IQuoteRepository quoteRepository,
            IMapper mapper,
            ILogger<GetAllQuotesQueryHandler> logger)
        {
            _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllQuotesQuery by retrieving all active quotes from the repository with their related entities.
        /// </summary>
        /// <param name="request">The GetAllQuotesQuery request.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of QuoteListDto objects representing all active quotes with status and line item information.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<IEnumerable<QuoteListDto>> Handle(GetAllQuotesQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                // Retrieve all active quotes from the repository
                var quotes = await _quoteRepository.GetAllAsync(cancellationToken);

                // For each quote, load its related entities
                foreach (var quote in quotes)
                {
                    // Load the quote status
                    if (quote.QuoteStatusId.HasValue)
                    {
                        await _quoteRepository.LoadQuoteStatusAsync(quote, cancellationToken);
                    }

                    // Load the line items for calculating the total amount and line item count
                    await _quoteRepository.LoadLineItemsAsync(quote, cancellationToken);
                }

                // Map to DTOs
                var quoteDtos = _mapper.Map<IEnumerable<QuoteListDto>>(quotes);

                _logger.LogInformation("Retrieved {Count} quotes with their related entities", quotes.Count());

                return quoteDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all quotes");
                throw;
            }
        }
    }
}
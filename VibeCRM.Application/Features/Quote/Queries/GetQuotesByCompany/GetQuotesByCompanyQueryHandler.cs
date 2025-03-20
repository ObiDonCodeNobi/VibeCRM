using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Quote.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Quote.Queries.GetQuotesByCompany
{
    /// <summary>
    /// Handler for processing GetQuotesByCompanyQuery requests.
    /// Implements the CQRS query handler pattern for retrieving quotes associated with a specific company.
    /// </summary>
    public class GetQuotesByCompanyQueryHandler : IRequestHandler<GetQuotesByCompanyQuery, IEnumerable<QuoteListDto>>
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetQuotesByCompanyQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetQuotesByCompanyQueryHandler"/> class.
        /// </summary>
        /// <param name="quoteRepository">The quote repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetQuotesByCompanyQueryHandler(
            IQuoteRepository quoteRepository,
            IMapper mapper,
            ILogger<GetQuotesByCompanyQueryHandler> logger)
        {
            _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetQuotesByCompanyQuery by retrieving quotes associated with a specific company along with their related entities.
        /// </summary>
        /// <param name="request">The GetQuotesByCompanyQuery containing the company ID to retrieve quotes for.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of QuoteListDto objects representing the quotes associated with the specified company, including status and line item information.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<IEnumerable<QuoteListDto>> Handle(GetQuotesByCompanyQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                // Retrieve quotes by company ID
                var quotes = await _quoteRepository.GetByCompanyAsync(request.CompanyId, cancellationToken);

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

                _logger.LogInformation("Retrieved {Count} quotes with related entities for company ID: {CompanyId}", quotes.Count(), request.CompanyId);

                return quoteDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving quotes for company ID: {CompanyId}", request.CompanyId);
                throw;
            }
        }
    }
}
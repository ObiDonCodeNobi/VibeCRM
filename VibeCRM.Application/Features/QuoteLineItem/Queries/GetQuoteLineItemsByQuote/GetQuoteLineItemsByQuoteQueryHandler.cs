using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.QuoteLineItem.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.QuoteLineItem.Queries.GetQuoteLineItemsByQuote
{
    /// <summary>
    /// Handler for processing the GetQuoteLineItemsByQuoteQuery
    /// </summary>
    public class GetQuoteLineItemsByQuoteQueryHandler : IRequestHandler<GetQuoteLineItemsByQuoteQuery, IEnumerable<QuoteLineItemDetailsDto>>
    {
        private readonly IQuoteLineItemRepository _quoteLineItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetQuoteLineItemsByQuoteQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetQuoteLineItemsByQuoteQueryHandler"/> class
        /// </summary>
        /// <param name="quoteLineItemRepository">The quote line item repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetQuoteLineItemsByQuoteQueryHandler(
            IQuoteLineItemRepository quoteLineItemRepository,
            IMapper mapper,
            ILogger<GetQuoteLineItemsByQuoteQueryHandler> logger)
        {
            _quoteLineItemRepository = quoteLineItemRepository ?? throw new ArgumentNullException(nameof(quoteLineItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetQuoteLineItemsByQuoteQuery
        /// </summary>
        /// <param name="request">The query to retrieve quote line items by quote ID</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collection of quote line items for the specified quote</returns>
        public async Task<IEnumerable<QuoteLineItemDetailsDto>> Handle(GetQuoteLineItemsByQuoteQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving quote line items for quote with ID {QuoteId}", request.QuoteId);

            try
            {
                var quoteLineItems = await _quoteLineItemRepository.GetByQuoteIdAsync(request.QuoteId, cancellationToken);

                _logger.LogInformation("Successfully retrieved quote line items for quote with ID {QuoteId}", request.QuoteId);

                return _mapper.Map<IEnumerable<QuoteLineItemDetailsDto>>(quoteLineItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving quote line items for quote with ID {QuoteId}", request.QuoteId);
                throw;
            }
        }
    }
}
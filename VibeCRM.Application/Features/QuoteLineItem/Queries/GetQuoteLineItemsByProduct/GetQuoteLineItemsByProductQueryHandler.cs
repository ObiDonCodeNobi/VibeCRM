using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.QuoteLineItem.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.QuoteLineItem.Queries.GetQuoteLineItemsByProduct
{
    /// <summary>
    /// Handler for processing the GetQuoteLineItemsByProductQuery
    /// </summary>
    public class GetQuoteLineItemsByProductQueryHandler : IRequestHandler<GetQuoteLineItemsByProductQuery, IEnumerable<QuoteLineItemDetailsDto>>
    {
        private readonly IQuoteLineItemRepository _quoteLineItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetQuoteLineItemsByProductQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetQuoteLineItemsByProductQueryHandler"/> class
        /// </summary>
        /// <param name="quoteLineItemRepository">The quote line item repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetQuoteLineItemsByProductQueryHandler(
            IQuoteLineItemRepository quoteLineItemRepository,
            IMapper mapper,
            ILogger<GetQuoteLineItemsByProductQueryHandler> logger)
        {
            _quoteLineItemRepository = quoteLineItemRepository ?? throw new ArgumentNullException(nameof(quoteLineItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetQuoteLineItemsByProductQuery
        /// </summary>
        /// <param name="request">The query to retrieve quote line items by product ID</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collection of quote line items for the specified product</returns>
        public async Task<IEnumerable<QuoteLineItemDetailsDto>> Handle(GetQuoteLineItemsByProductQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving quote line items for product with ID {ProductId}", request.ProductId);

            try
            {
                var quoteLineItems = await _quoteLineItemRepository.GetByProductIdAsync(request.ProductId, cancellationToken);

                _logger.LogInformation("Successfully retrieved quote line items for product with ID {ProductId}", request.ProductId);

                return _mapper.Map<IEnumerable<QuoteLineItemDetailsDto>>(quoteLineItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving quote line items for product with ID {ProductId}", request.ProductId);
                throw;
            }
        }
    }
}
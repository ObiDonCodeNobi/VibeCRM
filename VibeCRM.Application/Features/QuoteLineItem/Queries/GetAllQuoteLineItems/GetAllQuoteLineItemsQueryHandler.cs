using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.QuoteLineItem;

namespace VibeCRM.Application.Features.QuoteLineItem.Queries.GetAllQuoteLineItems
{
    /// <summary>
    /// Handler for processing the GetAllQuoteLineItemsQuery
    /// </summary>
    public class GetAllQuoteLineItemsQueryHandler : IRequestHandler<GetAllQuoteLineItemsQuery, IEnumerable<QuoteLineItemListDto>>
    {
        private readonly IQuoteLineItemRepository _quoteLineItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllQuoteLineItemsQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllQuoteLineItemsQueryHandler"/> class
        /// </summary>
        /// <param name="quoteLineItemRepository">The quote line item repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetAllQuoteLineItemsQueryHandler(
            IQuoteLineItemRepository quoteLineItemRepository,
            IMapper mapper,
            ILogger<GetAllQuoteLineItemsQueryHandler> logger)
        {
            _quoteLineItemRepository = quoteLineItemRepository ?? throw new ArgumentNullException(nameof(quoteLineItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllQuoteLineItemsQuery
        /// </summary>
        /// <param name="request">The query to retrieve all quote line items</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collection of quote line items</returns>
        public async Task<IEnumerable<QuoteLineItemListDto>> Handle(GetAllQuoteLineItemsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving all active quote line items");

            try
            {
                var quoteLineItems = await _quoteLineItemRepository.GetAllAsync(cancellationToken);

                _logger.LogInformation("Successfully retrieved all active quote line items");

                return _mapper.Map<IEnumerable<QuoteLineItemListDto>>(quoteLineItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all active quote line items");
                throw;
            }
        }
    }
}
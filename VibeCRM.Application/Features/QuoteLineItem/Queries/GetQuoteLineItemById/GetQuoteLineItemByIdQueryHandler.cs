using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.QuoteLineItem;

namespace VibeCRM.Application.Features.QuoteLineItem.Queries.GetQuoteLineItemById
{
    /// <summary>
    /// Handler for processing the GetQuoteLineItemByIdQuery
    /// </summary>
    public class GetQuoteLineItemByIdQueryHandler : IRequestHandler<GetQuoteLineItemByIdQuery, QuoteLineItemDetailsDto>
    {
        private readonly IQuoteLineItemRepository _quoteLineItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetQuoteLineItemByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetQuoteLineItemByIdQueryHandler"/> class
        /// </summary>
        /// <param name="quoteLineItemRepository">The quote line item repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetQuoteLineItemByIdQueryHandler(
            IQuoteLineItemRepository quoteLineItemRepository,
            IMapper mapper,
            ILogger<GetQuoteLineItemByIdQueryHandler> logger)
        {
            _quoteLineItemRepository = quoteLineItemRepository ?? throw new ArgumentNullException(nameof(quoteLineItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetQuoteLineItemByIdQuery by retrieving a quote line item by its ID from the database.
        /// </summary>
        /// <param name="request">The query containing the ID of the quote line item to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A QuoteLineItemDetailsDto representing the requested quote line item, or null if not found.</returns>
        public async Task<QuoteLineItemDetailsDto> Handle(GetQuoteLineItemByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving quote line item with ID {QuoteLineItemId}", request.Id);

            try
            {
                var quoteLineItem = await _quoteLineItemRepository.GetByIdAsync(request.Id, cancellationToken);

                if (quoteLineItem == null || !quoteLineItem.Active)
                {
                    _logger.LogWarning("Quote line item with ID {QuoteLineItemId} not found or inactive", request.Id);
                    return new QuoteLineItemDetailsDto();
                }

                _logger.LogInformation("Successfully retrieved quote line item with ID {QuoteLineItemId}", request.Id);

                return _mapper.Map<QuoteLineItemDetailsDto>(quoteLineItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving quote line item with ID {QuoteLineItemId}", request.Id);
                throw;
            }
        }
    }
}
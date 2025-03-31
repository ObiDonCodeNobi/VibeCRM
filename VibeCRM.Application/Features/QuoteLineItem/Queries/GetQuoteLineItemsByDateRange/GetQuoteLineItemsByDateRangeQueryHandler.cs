using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.QuoteLineItem;

namespace VibeCRM.Application.Features.QuoteLineItem.Queries.GetQuoteLineItemsByDateRange
{
    /// <summary>
    /// Handler for processing the GetQuoteLineItemsByDateRangeQuery
    /// </summary>
    public class GetQuoteLineItemsByDateRangeQueryHandler : IRequestHandler<GetQuoteLineItemsByDateRangeQuery, IEnumerable<QuoteLineItemDetailsDto>>
    {
        private readonly IQuoteLineItemRepository _quoteLineItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetQuoteLineItemsByDateRangeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetQuoteLineItemsByDateRangeQueryHandler"/> class
        /// </summary>
        /// <param name="quoteLineItemRepository">The quote line item repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetQuoteLineItemsByDateRangeQueryHandler(
            IQuoteLineItemRepository quoteLineItemRepository,
            IMapper mapper,
            ILogger<GetQuoteLineItemsByDateRangeQueryHandler> logger)
        {
            _quoteLineItemRepository = quoteLineItemRepository ?? throw new ArgumentNullException(nameof(quoteLineItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetQuoteLineItemsByDateRangeQuery
        /// </summary>
        /// <param name="request">The query to retrieve quote line items by date range</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collection of quote line items created within the specified date range</returns>
        public async Task<IEnumerable<QuoteLineItemDetailsDto>> Handle(GetQuoteLineItemsByDateRangeQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving quote line items created between {StartDate} and {EndDate}",
                request.StartDate.ToShortDateString(), request.EndDate.ToShortDateString());

            try
            {
                var quoteLineItems = await _quoteLineItemRepository.GetByDateRangeAsync(
                    request.StartDate, request.EndDate, cancellationToken);

                _logger.LogInformation("Successfully retrieved quote line items created between {StartDate} and {EndDate}",
                    request.StartDate.ToShortDateString(), request.EndDate.ToShortDateString());

                return _mapper.Map<IEnumerable<QuoteLineItemDetailsDto>>(quoteLineItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving quote line items created between {StartDate} and {EndDate}",
                    request.StartDate.ToShortDateString(), request.EndDate.ToShortDateString());
                throw;
            }
        }
    }
}
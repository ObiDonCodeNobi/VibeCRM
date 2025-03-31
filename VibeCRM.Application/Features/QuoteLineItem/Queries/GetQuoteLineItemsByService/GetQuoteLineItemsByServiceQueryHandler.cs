using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.QuoteLineItem;

namespace VibeCRM.Application.Features.QuoteLineItem.Queries.GetQuoteLineItemsByService
{
    /// <summary>
    /// Handler for processing the GetQuoteLineItemsByServiceQuery
    /// </summary>
    public class GetQuoteLineItemsByServiceQueryHandler : IRequestHandler<GetQuoteLineItemsByServiceQuery, IEnumerable<QuoteLineItemDetailsDto>>
    {
        private readonly IQuoteLineItemRepository _quoteLineItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetQuoteLineItemsByServiceQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetQuoteLineItemsByServiceQueryHandler"/> class
        /// </summary>
        /// <param name="quoteLineItemRepository">The quote line item repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public GetQuoteLineItemsByServiceQueryHandler(
            IQuoteLineItemRepository quoteLineItemRepository,
            IMapper mapper,
            ILogger<GetQuoteLineItemsByServiceQueryHandler> logger)
        {
            _quoteLineItemRepository = quoteLineItemRepository ?? throw new ArgumentNullException(nameof(quoteLineItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetQuoteLineItemsByServiceQuery
        /// </summary>
        /// <param name="request">The query to retrieve quote line items by service ID</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A collection of quote line items for the specified service</returns>
        public async Task<IEnumerable<QuoteLineItemDetailsDto>> Handle(GetQuoteLineItemsByServiceQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving quote line items for service with ID {ServiceId}", request.ServiceId);

            try
            {
                var quoteLineItems = await _quoteLineItemRepository.GetByServiceIdAsync(request.ServiceId, cancellationToken);

                _logger.LogInformation("Successfully retrieved quote line items for service with ID {ServiceId}", request.ServiceId);

                return _mapper.Map<IEnumerable<QuoteLineItemDetailsDto>>(quoteLineItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving quote line items for service with ID {ServiceId}", request.ServiceId);
                throw;
            }
        }
    }
}
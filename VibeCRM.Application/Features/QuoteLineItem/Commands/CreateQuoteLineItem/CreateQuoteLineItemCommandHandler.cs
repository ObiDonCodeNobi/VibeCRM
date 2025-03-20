using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.QuoteLineItem.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.QuoteLineItem.Commands.CreateQuoteLineItem
{
    /// <summary>
    /// Handler for processing the CreateQuoteLineItemCommand
    /// </summary>
    public class CreateQuoteLineItemCommandHandler : IRequestHandler<CreateQuoteLineItemCommand, QuoteLineItemDetailsDto>
    {
        private readonly IQuoteLineItemRepository _quoteLineItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateQuoteLineItemCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateQuoteLineItemCommandHandler"/> class
        /// </summary>
        /// <param name="quoteLineItemRepository">The quote line item repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">The logger</param>
        public CreateQuoteLineItemCommandHandler(
            IQuoteLineItemRepository quoteLineItemRepository,
            IMapper mapper,
            ILogger<CreateQuoteLineItemCommandHandler> logger)
        {
            _quoteLineItemRepository = quoteLineItemRepository ?? throw new ArgumentNullException(nameof(quoteLineItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateQuoteLineItemCommand
        /// </summary>
        /// <param name="request">The command to create a quote line item</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The created quote line item details</returns>
        public async Task<QuoteLineItemDetailsDto> Handle(CreateQuoteLineItemCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating new quote line item for quote {QuoteId}", request.QuoteId);

            try
            {
                var quoteLineItem = _mapper.Map<Domain.Entities.BusinessEntities.QuoteLineItem>(request);

                // Set audit properties
                quoteLineItem.CreatedBy = request.CreatedBy;
                quoteLineItem.CreatedDate = DateTime.UtcNow;
                quoteLineItem.ModifiedBy = request.CreatedBy;
                quoteLineItem.ModifiedDate = DateTime.UtcNow;
                quoteLineItem.Active = true;

                var createdQuoteLineItem = await _quoteLineItemRepository.AddAsync(quoteLineItem, cancellationToken);

                _logger.LogInformation("Successfully created quote line item with ID {QuoteLineItemId}", createdQuoteLineItem.QuoteLineItemId);

                return _mapper.Map<QuoteLineItemDetailsDto>(createdQuoteLineItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating quote line item for quote {QuoteId}", request.QuoteId);
                throw;
            }
        }
    }
}
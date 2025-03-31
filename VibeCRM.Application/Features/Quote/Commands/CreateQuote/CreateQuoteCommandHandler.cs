using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Quote;

namespace VibeCRM.Application.Features.Quote.Commands.CreateQuote
{
    /// <summary>
    /// Handler for processing CreateQuoteCommand requests.
    /// Implements the CQRS command handler pattern for creating quote entities.
    /// </summary>
    public class CreateQuoteCommandHandler : IRequestHandler<CreateQuoteCommand, QuoteDetailsDto>
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateQuoteCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateQuoteCommandHandler"/> class.
        /// </summary>
        /// <param name="quoteRepository">The quote repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public CreateQuoteCommandHandler(
            IQuoteRepository quoteRepository,
            IMapper mapper,
            ILogger<CreateQuoteCommandHandler> logger)
        {
            _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateQuoteCommand by creating a new quote entity in the database.
        /// </summary>
        /// <param name="request">The CreateQuoteCommand containing the quote details to create.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A QuoteDetailsDto representing the newly created quote with all related entities.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<QuoteDetailsDto> Handle(CreateQuoteCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                // Map command to entity
                var quoteEntity = _mapper.Map<Domain.Entities.BusinessEntities.Quote>(request);

                // Set creation and modification dates
                quoteEntity.CreatedDate = DateTime.UtcNow;
                quoteEntity.ModifiedDate = DateTime.UtcNow;

                // Create the quote in the repository
                var createdQuote = await _quoteRepository.AddAsync(quoteEntity, cancellationToken);

                // Load related entities for the created quote
                if (createdQuote.QuoteStatusId.HasValue)
                {
                    await _quoteRepository.LoadQuoteStatusAsync(createdQuote, cancellationToken);
                }

                // Initialize empty collections for LineItems and SalesOrders
                // These will be empty for a new quote but we load them to maintain consistency
                await _quoteRepository.LoadLineItemsAsync(createdQuote, cancellationToken);
                await _quoteRepository.LoadSalesOrdersAsync(createdQuote, cancellationToken);

                _logger.LogInformation("Created new quote with ID: {QuoteId}", createdQuote.QuoteId);

                // Map the created entity back to DTO
                return _mapper.Map<QuoteDetailsDto>(createdQuote);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new quote with number: {QuoteNumber}", request.Number);
                throw;
            }
        }
    }
}
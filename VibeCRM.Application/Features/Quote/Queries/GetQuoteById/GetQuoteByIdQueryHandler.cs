using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Quote.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Quote.Queries.GetQuoteById
{
    /// <summary>
    /// Handler for processing GetQuoteByIdQuery requests.
    /// Implements the CQRS query handler pattern for retrieving a specific quote by ID.
    /// </summary>
    public class GetQuoteByIdQueryHandler : IRequestHandler<GetQuoteByIdQuery, QuoteDetailsDto>
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetQuoteByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetQuoteByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="quoteRepository">The quote repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetQuoteByIdQueryHandler(
            IQuoteRepository quoteRepository,
            IMapper mapper,
            ILogger<GetQuoteByIdQueryHandler> logger)
        {
            _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetQuoteByIdQuery by retrieving a quote by its ID from the database.
        /// </summary>
        /// <param name="request">The query containing the ID of the quote to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A QuoteDetailsDto representing the requested quote, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<QuoteDetailsDto> Handle(GetQuoteByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                // Retrieve the quote by ID with all related entities
                var quote = await _quoteRepository.GetByIdWithRelatedEntitiesAsync(request.Id, cancellationToken);

                // Return null if the quote is not found or inactive
                if (quote == null || !quote.Active)
                {
                    _logger.LogWarning("Quote not found with ID: {QuoteId}", request.Id);
                    return new QuoteDetailsDto();
                }

                // Map to DTO
                var quoteDto = _mapper.Map<QuoteDetailsDto>(quote);

                _logger.LogInformation("Retrieved quote with ID: {QuoteId} with all related entities", request.Id);

                return quoteDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving quote with ID: {QuoteId}", request.Id);
                throw;
            }
        }
    }
}
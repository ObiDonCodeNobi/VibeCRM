using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.QuoteStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.QuoteStatus.Queries.GetQuoteStatusByOrdinalPosition
{
    /// <summary>
    /// Handler for the GetQuoteStatusByOrdinalPositionQuery.
    /// Retrieves quote statuses by their ordinal position from the database.
    /// </summary>
    public class GetQuoteStatusByOrdinalPositionQueryHandler : IRequestHandler<GetQuoteStatusByOrdinalPositionQuery, IEnumerable<QuoteStatusDto>>
    {
        private readonly IQuoteStatusRepository _quoteStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetQuoteStatusByOrdinalPositionQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetQuoteStatusByOrdinalPositionQueryHandler"/> class.
        /// </summary>
        /// <param name="quoteStatusRepository">The quote status repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetQuoteStatusByOrdinalPositionQueryHandler(
            IQuoteStatusRepository quoteStatusRepository,
            IMapper mapper,
            ILogger<GetQuoteStatusByOrdinalPositionQueryHandler> logger)
        {
            _quoteStatusRepository = quoteStatusRepository ?? throw new ArgumentNullException(nameof(quoteStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetQuoteStatusByOrdinalPositionQuery by retrieving quote statuses by their ordinal position.
        /// </summary>
        /// <param name="request">The query request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of quote status DTOs with the specified ordinal position.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<IEnumerable<QuoteStatusDto>> Handle(GetQuoteStatusByOrdinalPositionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving quote statuses with ordinal position: {OrdinalPosition}", request.OrdinalPosition);

                // Get quote statuses by ordinal position
                var quoteStatuses = await _quoteStatusRepository.GetByOrdinalPositionAsync(cancellationToken);

                // Filter by the requested ordinal position
                var filteredQuoteStatuses = quoteStatuses.Where(qs => qs.OrdinalPosition == request.OrdinalPosition);

                // Map to DTOs
                var quoteStatusDtos = _mapper.Map<IEnumerable<QuoteStatusDto>>(filteredQuoteStatuses);

                _logger.LogInformation("Successfully retrieved {Count} quote statuses with ordinal position {OrdinalPosition}",
                    quoteStatusDtos.Count(), request.OrdinalPosition);

                return quoteStatusDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving quote statuses by ordinal position: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
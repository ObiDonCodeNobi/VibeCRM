using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.QuoteStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.QuoteStatus.Queries.GetAllQuoteStatuses
{
    /// <summary>
    /// Handler for the GetAllQuoteStatusesQuery.
    /// Retrieves all active quote statuses from the database.
    /// </summary>
    public class GetAllQuoteStatusesQueryHandler : IRequestHandler<GetAllQuoteStatusesQuery, IEnumerable<QuoteStatusListDto>>
    {
        private readonly IQuoteStatusRepository _quoteStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllQuoteStatusesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllQuoteStatusesQueryHandler"/> class.
        /// </summary>
        /// <param name="quoteStatusRepository">The quote status repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetAllQuoteStatusesQueryHandler(
            IQuoteStatusRepository quoteStatusRepository,
            IMapper mapper,
            ILogger<GetAllQuoteStatusesQueryHandler> logger)
        {
            _quoteStatusRepository = quoteStatusRepository ?? throw new ArgumentNullException(nameof(quoteStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllQuoteStatusesQuery by retrieving all active quote statuses.
        /// </summary>
        /// <param name="request">The query request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of quote status list DTOs.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<IEnumerable<QuoteStatusListDto>> Handle(GetAllQuoteStatusesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving all quote statuses");

                // Get all quote statuses
                var quoteStatuses = await _quoteStatusRepository.GetAllAsync(cancellationToken);

                // Map to DTOs
                var quoteStatusDtos = _mapper.Map<IEnumerable<QuoteStatusListDto>>(quoteStatuses);

                _logger.LogInformation("Successfully retrieved {Count} quote statuses", quoteStatusDtos.Count());

                return quoteStatusDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all quote statuses: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
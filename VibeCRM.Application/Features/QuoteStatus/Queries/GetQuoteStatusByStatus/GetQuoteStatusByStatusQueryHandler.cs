using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.QuoteStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.QuoteStatus.Queries.GetQuoteStatusByStatus
{
    /// <summary>
    /// Handler for the GetQuoteStatusByStatusQuery.
    /// Retrieves quote statuses by their status name from the database.
    /// </summary>
    public class GetQuoteStatusByStatusQueryHandler : IRequestHandler<GetQuoteStatusByStatusQuery, IEnumerable<QuoteStatusDto>>
    {
        private readonly IQuoteStatusRepository _quoteStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetQuoteStatusByStatusQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetQuoteStatusByStatusQueryHandler"/> class.
        /// </summary>
        /// <param name="quoteStatusRepository">The quote status repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetQuoteStatusByStatusQueryHandler(
            IQuoteStatusRepository quoteStatusRepository,
            IMapper mapper,
            ILogger<GetQuoteStatusByStatusQueryHandler> logger)
        {
            _quoteStatusRepository = quoteStatusRepository ?? throw new ArgumentNullException(nameof(quoteStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetQuoteStatusByStatusQuery by retrieving quote statuses by their status name.
        /// </summary>
        /// <param name="request">The query request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of quote status DTOs with the specified status name.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<IEnumerable<QuoteStatusDto>> Handle(GetQuoteStatusByStatusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving quote statuses with status name: {StatusName}", request.Status);

                // Get quote statuses by status name
                var quoteStatuses = await _quoteStatusRepository.GetByStatusAsync(request.Status, cancellationToken);

                // Map to DTOs
                var quoteStatusDtos = _mapper.Map<IEnumerable<QuoteStatusDto>>(quoteStatuses);

                _logger.LogInformation("Successfully retrieved {Count} quote statuses with status name {StatusName}",
                    quoteStatusDtos.Count(), request.Status);

                return quoteStatusDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving quote statuses by status name: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
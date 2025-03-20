using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.QuoteStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.QuoteStatus.Queries.GetDefaultQuoteStatus
{
    /// <summary>
    /// Handler for the GetDefaultQuoteStatusQuery.
    /// Retrieves the default quote status from the database.
    /// </summary>
    public class GetDefaultQuoteStatusQueryHandler : IRequestHandler<GetDefaultQuoteStatusQuery, QuoteStatusDto>
    {
        private readonly IQuoteStatusRepository _quoteStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDefaultQuoteStatusQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultQuoteStatusQueryHandler"/> class.
        /// </summary>
        /// <param name="quoteStatusRepository">The quote status repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetDefaultQuoteStatusQueryHandler(
            IQuoteStatusRepository quoteStatusRepository,
            IMapper mapper,
            ILogger<GetDefaultQuoteStatusQueryHandler> logger)
        {
            _quoteStatusRepository = quoteStatusRepository ?? throw new ArgumentNullException(nameof(quoteStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetDefaultQuoteStatusQuery by retrieving the default quote status.
        /// </summary>
        /// <param name="request">The query request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The default quote status DTO if found, otherwise null.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when no default quote status is found.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<QuoteStatusDto> Handle(GetDefaultQuoteStatusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving default quote status");

                // Get default quote status
                var defaultQuoteStatus = await _quoteStatusRepository.GetDefaultAsync(cancellationToken);
                
                if (defaultQuoteStatus == null)
                {
                    _logger.LogError("Default quote status not found");
                    throw new KeyNotFoundException("Default quote status not found");
                }

                // Map to DTO
                var quoteStatusDto = _mapper.Map<QuoteStatusDto>(defaultQuoteStatus);

                _logger.LogInformation("Successfully retrieved default quote status with ID: {QuoteStatusId}", quoteStatusDto.Id);

                return quoteStatusDto;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving default quote status: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}

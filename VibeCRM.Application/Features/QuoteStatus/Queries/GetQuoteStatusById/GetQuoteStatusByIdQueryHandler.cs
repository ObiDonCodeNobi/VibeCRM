using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.QuoteStatus;

namespace VibeCRM.Application.Features.QuoteStatus.Queries.GetQuoteStatusById
{
    /// <summary>
    /// Handler for the GetQuoteStatusByIdQuery.
    /// Retrieves a specific quote status by its ID from the database.
    /// </summary>
    public class GetQuoteStatusByIdQueryHandler : IRequestHandler<GetQuoteStatusByIdQuery, QuoteStatusDetailsDto>
    {
        private readonly IQuoteStatusRepository _quoteStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetQuoteStatusByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetQuoteStatusByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="quoteStatusRepository">The quote status repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetQuoteStatusByIdQueryHandler(
            IQuoteStatusRepository quoteStatusRepository,
            IMapper mapper,
            ILogger<GetQuoteStatusByIdQueryHandler> logger)
        {
            _quoteStatusRepository = quoteStatusRepository ?? throw new ArgumentNullException(nameof(quoteStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetQuoteStatusByIdQuery by retrieving a specific quote status by its ID.
        /// </summary>
        /// <param name="request">The query request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The quote status details DTO if found, otherwise null.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when no quote status with the specified ID is found.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<QuoteStatusDetailsDto> Handle(GetQuoteStatusByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving quote status with ID: {QuoteStatusId}", request.Id);

                // Get quote status by ID
                var quoteStatus = await _quoteStatusRepository.GetByIdAsync(request.Id, cancellationToken);

                if (quoteStatus == null)
                {
                    _logger.LogError("Quote status with ID {QuoteStatusId} not found", request.Id);
                    throw new KeyNotFoundException($"Quote status with ID {request.Id} not found");
                }

                // Map to DTO
                var quoteStatusDto = _mapper.Map<QuoteStatusDetailsDto>(quoteStatus);

                _logger.LogInformation("Successfully retrieved quote status with ID: {QuoteStatusId}", quoteStatusDto.Id);

                return quoteStatusDto;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving quote status by ID: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.QuoteStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.QuoteStatus.Commands.CreateQuoteStatus
{
    /// <summary>
    /// Handler for the CreateQuoteStatusCommand.
    /// Creates a new quote status in the database.
    /// </summary>
    public class CreateQuoteStatusCommandHandler : IRequestHandler<CreateQuoteStatusCommand, QuoteStatusDto>
    {
        private readonly IQuoteStatusRepository _quoteStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateQuoteStatusCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateQuoteStatusCommandHandler"/> class.
        /// </summary>
        /// <param name="quoteStatusRepository">The quote status repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public CreateQuoteStatusCommandHandler(
            IQuoteStatusRepository quoteStatusRepository,
            IMapper mapper,
            ILogger<CreateQuoteStatusCommandHandler> logger)
        {
            _quoteStatusRepository = quoteStatusRepository ?? throw new ArgumentNullException(nameof(quoteStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateQuoteStatusCommand by creating a new quote status in the database.
        /// </summary>
        /// <param name="request">The command request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created quote status DTO.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the creation process.</exception>
        public async Task<QuoteStatusDto> Handle(CreateQuoteStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating new quote status with name: {StatusName}", request.Status);

                // Map command to entity
                var quoteStatusEntity = _mapper.Map<Domain.Entities.TypeStatusEntities.QuoteStatus>(request);
                
                // Set audit fields
                quoteStatusEntity.CreatedDate = DateTime.UtcNow;
                quoteStatusEntity.ModifiedDate = DateTime.UtcNow;
                quoteStatusEntity.Active = true;

                // Create quote status
                var createdQuoteStatus = await _quoteStatusRepository.AddAsync(quoteStatusEntity, cancellationToken);
                
                // Map to DTO
                var quoteStatusDto = _mapper.Map<QuoteStatusDto>(createdQuoteStatus);

                _logger.LogInformation("Successfully created quote status with ID: {QuoteStatusId}", quoteStatusDto.Id);

                return quoteStatusDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating quote status: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}

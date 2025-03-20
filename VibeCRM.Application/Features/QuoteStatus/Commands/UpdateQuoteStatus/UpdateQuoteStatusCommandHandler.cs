using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.QuoteStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.QuoteStatus.Commands.UpdateQuoteStatus
{
    /// <summary>
    /// Handler for the UpdateQuoteStatusCommand.
    /// Updates an existing quote status in the database.
    /// </summary>
    public class UpdateQuoteStatusCommandHandler : IRequestHandler<UpdateQuoteStatusCommand, QuoteStatusDto>
    {
        private readonly IQuoteStatusRepository _quoteStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateQuoteStatusCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateQuoteStatusCommandHandler"/> class.
        /// </summary>
        /// <param name="quoteStatusRepository">The quote status repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public UpdateQuoteStatusCommandHandler(
            IQuoteStatusRepository quoteStatusRepository,
            IMapper mapper,
            ILogger<UpdateQuoteStatusCommandHandler> logger)
        {
            _quoteStatusRepository = quoteStatusRepository ?? throw new ArgumentNullException(nameof(quoteStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateQuoteStatusCommand by updating an existing quote status in the database.
        /// </summary>
        /// <param name="request">The command request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated quote status DTO.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the quote status to update is not found.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the update process.</exception>
        public async Task<QuoteStatusDto> Handle(UpdateQuoteStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating quote status with ID: {QuoteStatusId}", request.Id);

                // Get existing quote status
                var existingQuoteStatus = await _quoteStatusRepository.GetByIdAsync(request.Id, cancellationToken);

                if (existingQuoteStatus == null)
                {
                    _logger.LogError("Quote status with ID {QuoteStatusId} not found", request.Id);
                    throw new KeyNotFoundException($"Quote status with ID {request.Id} not found");
                }

                // Update properties
                _mapper.Map(request, existingQuoteStatus);

                // Update audit fields
                existingQuoteStatus.ModifiedDate = DateTime.UtcNow;

                // Update quote status
                var updatedQuoteStatus = await _quoteStatusRepository.UpdateAsync(existingQuoteStatus, cancellationToken);

                // Map to DTO
                var quoteStatusDto = _mapper.Map<QuoteStatusDto>(updatedQuoteStatus);

                _logger.LogInformation("Successfully updated quote status with ID: {QuoteStatusId}", quoteStatusDto.Id);

                return quoteStatusDto;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating quote status: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Call;

namespace VibeCRM.Application.Features.Call.Commands.UpdateCall
{
    /// <summary>
    /// Handler for processing UpdateCallCommand requests.
    /// Implements the CQRS command handler pattern for updating call entities.
    /// </summary>
    public class UpdateCallCommandHandler : IRequestHandler<UpdateCallCommand, CallDto>
    {
        private readonly ICallRepository _callRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCallCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCallCommandHandler"/> class.
        /// </summary>
        /// <param name="callRepository">The call repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public UpdateCallCommandHandler(
            ICallRepository callRepository,
            IMapper mapper,
            ILogger<UpdateCallCommandHandler> logger)
        {
            _callRepository = callRepository ?? throw new ArgumentNullException(nameof(callRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateCallCommand by updating an existing call entity in the database.
        /// </summary>
        /// <param name="request">The UpdateCallCommand containing the call data to update.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A CallDto representing the updated call.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the call ID is empty.</exception>
        public async Task<CallDto> Handle(UpdateCallCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Id == Guid.Empty) throw new ArgumentException("Call ID cannot be empty", nameof(request.Id));

            try
            {
                _logger.LogInformation("Updating call with ID: {CallId}", request.Id);

                // Get the existing call (Active=1 filter is applied in the repository)
                var existingCall = await _callRepository.GetByIdAsync(request.Id, cancellationToken);

                if (existingCall == null)
                {
                    _logger.LogWarning("Call with ID {CallId} not found or is inactive", request.Id);
                    throw new InvalidOperationException($"Call with ID {request.Id} not found or is inactive");
                }

                // Map the updated properties while preserving existing ones
                _mapper.Map(request, existingCall);

                // Update audit fields
                existingCall.ModifiedDate = DateTime.UtcNow;

                // Save to database
                var updatedCall = await _callRepository.UpdateAsync(existingCall, cancellationToken);

                _logger.LogInformation("Successfully updated call with ID: {CallId}", updatedCall.CallId);

                // Map the entity back to a DTO for the response
                return _mapper.Map<CallDto>(updatedCall);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating call with ID {CallId}: {ErrorMessage}",
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}
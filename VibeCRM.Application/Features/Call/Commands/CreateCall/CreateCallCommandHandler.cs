using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Call;

namespace VibeCRM.Application.Features.Call.Commands.CreateCall
{
    /// <summary>
    /// Handler for processing CreateCallCommand requests.
    /// Implements the CQRS command handler pattern for creating call entities.
    /// </summary>
    public class CreateCallCommandHandler : IRequestHandler<CreateCallCommand, CallDto>
    {
        private readonly ICallRepository _callRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCallCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCallCommandHandler"/> class.
        /// </summary>
        /// <param name="callRepository">The call repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public CreateCallCommandHandler(
            ICallRepository callRepository,
            IMapper mapper,
            ILogger<CreateCallCommandHandler> logger)
        {
            _callRepository = callRepository ?? throw new ArgumentNullException(nameof(callRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateCallCommand by creating a new call entity in the database.
        /// </summary>
        /// <param name="request">The CreateCallCommand containing the call data to create.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A CallDto representing the newly created call.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<CallDto> Handle(CreateCallCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Creating new call with ID: {CallId}", request.Id);

                // Map the command to an entity
                var callEntity = _mapper.Map<Domain.Entities.BusinessEntities.Call>(request);

                // Set audit fields
                callEntity.CreatedDate = DateTime.UtcNow;
                callEntity.ModifiedDate = DateTime.UtcNow;
                callEntity.Active = true;

                // Save to database
                var createdCall = await _callRepository.AddAsync(callEntity, cancellationToken);

                _logger.LogInformation("Successfully created call with ID: {CallId}", createdCall.CallId);

                // Map the entity back to a DTO for the response
                return _mapper.Map<CallDto>(createdCall);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating call with ID {CallId}: {ErrorMessage}",
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}
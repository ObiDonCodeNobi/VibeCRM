using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Call.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Application.Common.Exceptions;

namespace VibeCRM.Application.Features.Call.Queries.GetCallById
{
    /// <summary>
    /// Handler for processing GetCallByIdQuery requests.
    /// Implements the CQRS query handler pattern for retrieving a specific call.
    /// </summary>
    public class GetCallByIdQueryHandler : IRequestHandler<GetCallByIdQuery, CallDetailsDto>
    {
        private readonly ICallRepository _callRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCallByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCallByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="callRepository">The call repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public GetCallByIdQueryHandler(
            ICallRepository callRepository,
            IMapper mapper,
            ILogger<GetCallByIdQueryHandler> logger)
        {
            _callRepository = callRepository ?? throw new ArgumentNullException(nameof(callRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetCallByIdQuery by retrieving a specific call entity from the database.
        /// </summary>
        /// <param name="request">The GetCallByIdQuery containing the call ID to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A CallDetailsDto containing the requested call's data, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the call ID is empty.</exception>
        public async Task<CallDetailsDto> Handle(GetCallByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Id == Guid.Empty) throw new ArgumentException("Call ID cannot be empty", nameof(request.Id));

            try
            {
                _logger.LogInformation("Retrieving call with ID: {CallId}", request.Id);

                // Get the call from the repository (Active=1 filter is applied in the repository)
                var call = await _callRepository.GetByIdAsync(request.Id, cancellationToken);

                if (call == null)
                {
                    _logger.LogWarning("Call with ID {CallId} not found", request.Id);
                    throw new NotFoundException(nameof(Domain.Entities.BusinessEntities.Call), request.Id);
                }

                // Map the entity to a DTO
                var callDto = _mapper.Map<CallDetailsDto>(call);

                _logger.LogInformation("Successfully retrieved call with ID: {CallId}", request.Id);

                return callDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving call with ID {CallId}: {ErrorMessage}",
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}
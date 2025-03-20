using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Call.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Call.Queries.GetAllCalls
{
    /// <summary>
    /// Handler for processing GetAllCallsQuery requests.
    /// Implements the CQRS query handler pattern for retrieving all active calls.
    /// </summary>
    public class GetAllCallsQueryHandler : IRequestHandler<GetAllCallsQuery, IEnumerable<CallListDto>>
    {
        private readonly ICallRepository _callRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllCallsQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllCallsQueryHandler"/> class.
        /// </summary>
        /// <param name="callRepository">The call repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public GetAllCallsQueryHandler(
            ICallRepository callRepository,
            IMapper mapper,
            ILogger<GetAllCallsQueryHandler> logger)
        {
            _callRepository = callRepository ?? throw new ArgumentNullException(nameof(callRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllCallsQuery by retrieving all active call entities from the database.
        /// </summary>
        /// <param name="request">The GetAllCallsQuery request.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of CallListDto objects representing all active calls.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<IEnumerable<CallListDto>> Handle(GetAllCallsQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving all active calls");

                // Get all active calls from the repository (Active=1 filter is applied in the repository)
                var calls = await _callRepository.GetAllAsync(cancellationToken);

                // Map the entities to DTOs
                var callDtos = _mapper.Map<IEnumerable<CallListDto>>(calls);

                _logger.LogInformation("Successfully retrieved all active calls");

                return callDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all active calls: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
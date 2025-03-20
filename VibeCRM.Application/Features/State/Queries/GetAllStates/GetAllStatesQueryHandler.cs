using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.State.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.State.Queries.GetAllStates
{
    /// <summary>
    /// Handler for the GetAllStatesQuery.
    /// Retrieves all active states ordered by their ordinal position.
    /// </summary>
    public class GetAllStatesQueryHandler : IRequestHandler<GetAllStatesQuery, IEnumerable<StateListDto>>
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllStatesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllStatesQueryHandler"/> class.
        /// </summary>
        /// <param name="stateRepository">The state repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetAllStatesQueryHandler(
            IStateRepository stateRepository,
            IMapper mapper,
            ILogger<GetAllStatesQueryHandler> logger)
        {
            _stateRepository = stateRepository ?? throw new ArgumentNullException(nameof(stateRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllStatesQuery by retrieving all active states.
        /// </summary>
        /// <param name="request">The query for retrieving all states.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of state DTOs ordered by their ordinal position.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<IEnumerable<StateListDto>> Handle(GetAllStatesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving all states");

                // Get all states ordered by ordinal position
                var states = await _stateRepository.GetByOrdinalPositionAsync(cancellationToken);

                // Map to DTOs
                var stateDtos = _mapper.Map<IEnumerable<StateListDto>>(states);

                // Note: In a real application, you might want to populate AddressCount for each state
                // This would typically involve querying a separate repository or using a join in SQL

                _logger.LogInformation("Successfully retrieved all states");

                return stateDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all states: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
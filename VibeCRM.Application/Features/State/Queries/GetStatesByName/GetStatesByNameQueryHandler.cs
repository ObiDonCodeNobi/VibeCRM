using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.State.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.State.Queries.GetStatesByName
{
    /// <summary>
    /// Handler for the GetStatesByNameQuery.
    /// Retrieves states by their name or partial name match.
    /// </summary>
    public class GetStatesByNameQueryHandler : IRequestHandler<GetStatesByNameQuery, IEnumerable<StateListDto>>
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetStatesByNameQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetStatesByNameQueryHandler"/> class.
        /// </summary>
        /// <param name="stateRepository">The state repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetStatesByNameQueryHandler(
            IStateRepository stateRepository,
            IMapper mapper,
            ILogger<GetStatesByNameQueryHandler> logger)
        {
            _stateRepository = stateRepository ?? throw new ArgumentNullException(nameof(stateRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetStatesByNameQuery by retrieving states that match the provided name.
        /// </summary>
        /// <param name="request">The query for retrieving states by name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of state DTOs that match the search criteria.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<IEnumerable<StateListDto>> Handle(GetStatesByNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving states with name containing: {StateName}", request.Name);

                // Get states by name
                var states = await _stateRepository.GetByNameAsync(request.Name, cancellationToken);

                // Map to DTOs
                var stateDtos = _mapper.Map<IEnumerable<StateListDto>>(states);

                // Note: In a real application, you might want to populate AddressCount for each state
                // This would typically involve querying a separate repository or using a join in SQL

                _logger.LogInformation("Successfully retrieved states with name containing: {StateName}", request.Name);

                return stateDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving states with name containing {StateName}: {ErrorMessage}", request.Name, ex.Message);
                throw;
            }
        }
    }
}
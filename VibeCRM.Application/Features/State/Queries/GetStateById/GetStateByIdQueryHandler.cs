using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.State;

namespace VibeCRM.Application.Features.State.Queries.GetStateById
{
    /// <summary>
    /// Handler for the GetStateByIdQuery.
    /// Retrieves a state by its unique identifier.
    /// </summary>
    public class GetStateByIdQueryHandler : IRequestHandler<GetStateByIdQuery, StateDetailsDto>
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetStateByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetStateByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="stateRepository">The state repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetStateByIdQueryHandler(
            IStateRepository stateRepository,
            IMapper mapper,
            ILogger<GetStateByIdQueryHandler> logger)
        {
            _stateRepository = stateRepository ?? throw new ArgumentNullException(nameof(stateRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetStateByIdQuery by retrieving a state by its ID from the database.
        /// </summary>
        /// <param name="request">The query containing the ID of the state to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A StateDetailsDto representing the requested state, or null if not found.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public async Task<StateDetailsDto> Handle(GetStateByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving state with ID: {StateId}", request.Id);

                // Get state by ID
                var state = await _stateRepository.GetByIdAsync(request.Id, cancellationToken);
                if (state == null)
                {
                    _logger.LogWarning("State with ID: {StateId} not found", request.Id);
                    return new StateDetailsDto();
                }

                // Map to DTO
                var stateDto = _mapper.Map<StateDetailsDto>(state);

                // Note: In a real application, you might want to populate AddressCount for the state
                // This would typically involve querying a separate repository or using a join in SQL

                _logger.LogInformation("Successfully retrieved state with ID: {StateId}", stateDto.Id);

                return stateDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving state with ID {StateId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
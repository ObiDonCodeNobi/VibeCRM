using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.State;

namespace VibeCRM.Application.Features.State.Queries.GetDefaultState
{
    /// <summary>
    /// Handler for the GetDefaultStateQuery.
    /// Retrieves the default state for the application.
    /// </summary>
    public class GetDefaultStateQueryHandler : IRequestHandler<GetDefaultStateQuery, StateDto>
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDefaultStateQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultStateQueryHandler"/> class.
        /// </summary>
        /// <param name="stateRepository">The state repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetDefaultStateQueryHandler(
            IStateRepository stateRepository,
            IMapper mapper,
            ILogger<GetDefaultStateQueryHandler> logger)
        {
            _stateRepository = stateRepository ?? throw new ArgumentNullException(nameof(stateRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetDefaultStateQuery by retrieving the default state from the database.
        /// </summary>
        /// <param name="request">The query to retrieve the default state.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>The default state DTO if found, otherwise null.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        public async Task<StateDto> Handle(GetDefaultStateQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving default state");

                // Get default state
                var state = await _stateRepository.GetDefaultAsync(cancellationToken);
                if (state == null)
                {
                    _logger.LogWarning("Default state not found");
                    return new StateDto();
                }

                // Map to DTO
                var stateDto = _mapper.Map<StateDto>(state);

                _logger.LogInformation("Successfully retrieved default state: {StateName}", stateDto.Name);

                return stateDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving default state: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
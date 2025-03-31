using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.State;

namespace VibeCRM.Application.Features.State.Commands.UpdateState
{
    /// <summary>
    /// Handler for the UpdateStateCommand.
    /// Handles the updating of an existing state.
    /// </summary>
    public class UpdateStateCommandHandler : IRequestHandler<UpdateStateCommand, StateDto>
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateStateCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateStateCommandHandler"/> class.
        /// </summary>
        /// <param name="stateRepository">The state repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public UpdateStateCommandHandler(
            IStateRepository stateRepository,
            IMapper mapper,
            ILogger<UpdateStateCommandHandler> logger)
        {
            _stateRepository = stateRepository ?? throw new ArgumentNullException(nameof(stateRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateStateCommand by updating an existing state.
        /// </summary>
        /// <param name="request">The command for updating an existing state.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated state DTO.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the update process.</exception>
        public async Task<StateDto> Handle(UpdateStateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating state with ID: {StateId}", request.Id);

                // Check if the state exists
                var existingState = await _stateRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingState == null)
                {
                    _logger.LogWarning("State with ID: {StateId} not found", request.Id);
                    throw new Exception($"State with ID: {request.Id} not found");
                }

                // Map command to entity, preserving original values for fields not included in the update
                _mapper.Map(request, existingState);

                // Update audit fields
                existingState.ModifiedDate = DateTime.UtcNow;
                existingState.ModifiedBy = Guid.NewGuid(); // In a real app, this would be the current user ID

                // Update in repository
                var updatedState = await _stateRepository.UpdateAsync(existingState, cancellationToken);

                // Map back to DTO
                var stateDto = _mapper.Map<StateDto>(updatedState);

                _logger.LogInformation("Successfully updated state with ID: {StateId}", stateDto.Id);

                return stateDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating state with ID {StateId}: {ErrorMessage}", request.Id, ex.Message);
                throw;
            }
        }
    }
}
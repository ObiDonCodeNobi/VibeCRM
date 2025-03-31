using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.State;

namespace VibeCRM.Application.Features.State.Commands.CreateState
{
    /// <summary>
    /// Handler for the CreateStateCommand.
    /// Handles the creation of a new state.
    /// </summary>
    public class CreateStateCommandHandler : IRequestHandler<CreateStateCommand, StateDto>
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateStateCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateStateCommandHandler"/> class.
        /// </summary>
        /// <param name="stateRepository">The state repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public CreateStateCommandHandler(
            IStateRepository stateRepository,
            IMapper mapper,
            ILogger<CreateStateCommandHandler> logger)
        {
            _stateRepository = stateRepository ?? throw new ArgumentNullException(nameof(stateRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateStateCommand by creating a new state.
        /// </summary>
        /// <param name="request">The command for creating a new state.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The newly created state DTO.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the creation process.</exception>
        public async Task<StateDto> Handle(CreateStateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating new state with name: {StateName}", request.Name);

                // Map command to entity
                var stateEntity = _mapper.Map<Domain.Entities.TypeStatusEntities.State>(request);

                // Set audit fields
                stateEntity.CreatedDate = DateTime.UtcNow;
                stateEntity.CreatedBy = Guid.NewGuid(); // In a real app, this would be the current user ID
                stateEntity.ModifiedDate = stateEntity.CreatedDate;
                stateEntity.ModifiedBy = stateEntity.CreatedBy;
                stateEntity.Active = true;

                // Add to repository
                var createdState = await _stateRepository.AddAsync(stateEntity, cancellationToken);

                // Map back to DTO
                var stateDto = _mapper.Map<StateDto>(createdState);

                _logger.LogInformation("Successfully created state with ID: {StateId}", stateDto.Id);

                return stateDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating state: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
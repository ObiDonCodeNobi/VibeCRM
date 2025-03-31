using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.CallType;

namespace VibeCRM.Application.Features.CallType.Commands.CreateCallType
{
    /// <summary>
    /// Handler for processing the CreateCallTypeCommand.
    /// Implements IRequestHandler to handle the command and return a CallTypeDto.
    /// </summary>
    public class CreateCallTypeCommandHandler : IRequestHandler<CreateCallTypeCommand, CallTypeDto>
    {
        private readonly ICallTypeRepository _callTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCallTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCallTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="callTypeRepository">The call type repository for data access operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters is null.</exception>
        public CreateCallTypeCommandHandler(
            ICallTypeRepository callTypeRepository,
            IMapper mapper,
            ILogger<CreateCallTypeCommandHandler> logger)
        {
            _callTypeRepository = callTypeRepository ?? throw new ArgumentNullException(nameof(callTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateCallTypeCommand by creating a new call type in the database.
        /// </summary>
        /// <param name="request">The CreateCallTypeCommand containing the data for the new call type.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A CallTypeDto representing the newly created call type.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the call type could not be created.</exception>
        public async Task<CallTypeDto> Handle(CreateCallTypeCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Creating new call type: {Type}", request.Type);

            try
            {
                // Map the command to an entity
                var callTypeEntity = _mapper.Map<Domain.Entities.TypeStatusEntities.CallType>(request);

                // Set audit fields
                callTypeEntity.CreatedDate = DateTime.UtcNow;
                callTypeEntity.ModifiedDate = DateTime.UtcNow;
                callTypeEntity.Active = true;

                // Add the entity to the database
                var createdCallType = await _callTypeRepository.AddAsync(callTypeEntity, cancellationToken);

                // Map the entity back to a DTO and return it
                return _mapper.Map<CallTypeDto>(createdCallType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating call type: {Type}", request.Type);
                throw new InvalidOperationException($"Failed to create call type: {request.Type}", ex);
            }
        }
    }
}
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.CallType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.CallType.Commands.UpdateCallType
{
    /// <summary>
    /// Handler for processing the UpdateCallTypeCommand.
    /// Implements IRequestHandler to handle the command and return a CallTypeDto.
    /// </summary>
    public class UpdateCallTypeCommandHandler : IRequestHandler<UpdateCallTypeCommand, CallTypeDto>
    {
        private readonly ICallTypeRepository _callTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCallTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCallTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="callTypeRepository">The call type repository for data access operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters is null.</exception>
        public UpdateCallTypeCommandHandler(
            ICallTypeRepository callTypeRepository,
            IMapper mapper,
            ILogger<UpdateCallTypeCommandHandler> logger)
        {
            _callTypeRepository = callTypeRepository ?? throw new ArgumentNullException(nameof(callTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateCallTypeCommand by updating an existing call type in the database.
        /// </summary>
        /// <param name="request">The UpdateCallTypeCommand containing the data for the call type update.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A CallTypeDto representing the updated call type.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the call type could not be updated.</exception>
        public async Task<CallTypeDto> Handle(UpdateCallTypeCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Updating call type with ID: {Id}", request.Id);

            try
            {
                // Get the existing call type
                var existingCallType = await _callTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingCallType == null)
                {
                    throw new InvalidOperationException($"Call type with ID {request.Id} not found.");
                }

                // Map the command to the existing entity
                _mapper.Map(request, existingCallType);

                // Update modified date
                existingCallType.ModifiedDate = DateTime.UtcNow;

                // Update the entity in the database
                var updatedCallType = await _callTypeRepository.UpdateAsync(existingCallType, cancellationToken);

                // Map the entity back to a DTO and return it
                return _mapper.Map<CallTypeDto>(updatedCallType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating call type with ID: {Id}", request.Id);
                throw new InvalidOperationException($"Failed to update call type with ID: {request.Id}", ex);
            }
        }
    }
}
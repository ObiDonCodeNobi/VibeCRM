using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PersonType.Commands.UpdatePersonType
{
    /// <summary>
    /// Handler for the UpdatePersonTypeCommand.
    /// Processes requests to update existing person types in the system.
    /// </summary>
    public class UpdatePersonTypeCommandHandler : IRequestHandler<UpdatePersonTypeCommand, bool>
    {
        private readonly IPersonTypeRepository _personTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePersonTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the UpdatePersonTypeCommandHandler class.
        /// </summary>
        /// <param name="personTypeRepository">The repository for person type operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        public UpdatePersonTypeCommandHandler(
            IPersonTypeRepository personTypeRepository,
            IMapper mapper,
            ILogger<UpdatePersonTypeCommandHandler> logger)
        {
            _personTypeRepository = personTypeRepository ?? throw new ArgumentNullException(nameof(personTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdatePersonTypeCommand by updating an existing person type in the repository.
        /// </summary>
        /// <param name="request">The command containing the updated person type details.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>True if the person type was updated successfully; otherwise, false.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the person type could not be updated.</exception>
        public async Task<bool> Handle(UpdatePersonTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating person type with ID: {PersonTypeId}", request.Id);

                // Get existing entity
                var existingPersonType = await _personTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingPersonType == null)
                {
                    _logger.LogWarning("Person type with ID {PersonTypeId} not found", request.Id);
                    return false;
                }

                // Map updated properties
                _mapper.Map(request, existingPersonType);

                // Update in repository
                await _personTypeRepository.UpdateAsync(existingPersonType, cancellationToken);

                _logger.LogInformation("Successfully updated person type with ID: {PersonTypeId}", request.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating person type with ID: {PersonTypeId}. Error: {ErrorMessage}", 
                    request.Id, ex.Message);
                throw new InvalidOperationException($"Failed to update person type: {ex.Message}", ex);
            }
        }
    }
}

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PersonStatus.Commands.UpdatePersonStatus
{
    /// <summary>
    /// Handler for processing UpdatePersonStatusCommand requests.
    /// Implements the CQRS command handler pattern for updating person status entities.
    /// </summary>
    public class UpdatePersonStatusCommandHandler : IRequestHandler<UpdatePersonStatusCommand, bool>
    {
        private readonly IPersonStatusRepository _personStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePersonStatusCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePersonStatusCommandHandler"/> class.
        /// </summary>
        /// <param name="personStatusRepository">The person status repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public UpdatePersonStatusCommandHandler(
            IPersonStatusRepository personStatusRepository,
            IMapper mapper,
            ILogger<UpdatePersonStatusCommandHandler> logger)
        {
            _personStatusRepository = personStatusRepository ?? throw new ArgumentNullException(nameof(personStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdatePersonStatusCommand by updating an existing person status entity.
        /// </summary>
        /// <param name="request">The UpdatePersonStatusCommand containing the updated person status details.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<bool> Handle(UpdatePersonStatusCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Updating person status with ID: {PersonStatusId}", request.Id);

                // Check if the person status exists
                var existingPersonStatus = await _personStatusRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingPersonStatus == null)
                {
                    _logger.LogWarning("Person status with ID: {PersonStatusId} not found", request.Id);
                    return false;
                }

                // Map the command to the existing entity
                _mapper.Map(request, existingPersonStatus);
                
                // Ensure modified date is set
                if (existingPersonStatus.ModifiedDate == default)
                {
                    existingPersonStatus.ModifiedDate = DateTime.UtcNow;
                }

                // Update in repository
                await _personStatusRepository.UpdateAsync(existingPersonStatus, cancellationToken);

                _logger.LogInformation("Successfully updated person status with ID: {PersonStatusId}", request.Id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating person status with ID: {PersonStatusId}. Error: {ErrorMessage}", 
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}

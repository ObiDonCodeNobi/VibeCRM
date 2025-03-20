using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Person.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Person.Commands.UpdatePerson
{
    /// <summary>
    /// Handler for processing UpdatePersonCommand requests.
    /// Implements the CQRS command handler pattern for updating existing person entities.
    /// </summary>
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, PersonDetailsDto>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePersonCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePersonCommandHandler"/> class.
        /// </summary>
        /// <param name="personRepository">The person repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public UpdatePersonCommandHandler(
            IPersonRepository personRepository,
            IMapper mapper,
            ILogger<UpdatePersonCommandHandler> logger)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdatePersonCommand by updating an existing person entity in the database.
        /// </summary>
        /// <param name="request">The UpdatePersonCommand containing the updated person details.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A PersonDetailsDto representing the updated person.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the person to update is not found.</exception>
        public async Task<PersonDetailsDto> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            // Retrieve the existing person
            var existingPerson = await _personRepository.GetByIdAsync(request.Id, cancellationToken);
            if (existingPerson == null)
            {
                _logger.LogWarning("Person with ID {PersonId} not found for update", request.Id);
                throw new InvalidOperationException($"Person with ID {request.Id} not found.");
            }

            // Map command to entity, preserving values not included in the update
            var personToUpdate = _mapper.Map(request, existingPerson);

            try
            {
                // Update the person in the repository
                var updatedPerson = await _personRepository.UpdateAsync(personToUpdate, cancellationToken);
                _logger.LogInformation("Updated person with ID: {PersonId}", updatedPerson.PersonId);

                // Return the mapped DTO
                return _mapper.Map<PersonDetailsDto>(updatedPerson);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating person with ID: {PersonId}", request.Id);
                throw;
            }
        }
    }
}
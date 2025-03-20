using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Person.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Person.Commands.CreatePerson
{
    /// <summary>
    /// Handler for processing CreatePersonCommand requests.
    /// Implements the CQRS command handler pattern for creating new person entities.
    /// </summary>
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, PersonDetailsDto>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePersonCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePersonCommandHandler"/> class.
        /// </summary>
        /// <param name="personRepository">The person repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public CreatePersonCommandHandler(
            IPersonRepository personRepository,
            IMapper mapper,
            ILogger<CreatePersonCommandHandler> logger)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreatePersonCommand by creating a new person entity in the database.
        /// </summary>
        /// <param name="request">The CreatePersonCommand containing the person details.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A PersonDetailsDto representing the newly created person.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<PersonDetailsDto> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            // Map command to entity
            var person = _mapper.Map<VibeCRM.Domain.Entities.BusinessEntities.Person>(request);

            try
            {
                // Add the person to the repository
                var createdPerson = await _personRepository.AddAsync(person, cancellationToken);
                _logger.LogInformation("Created new person with ID: {PersonId}", createdPerson.PersonId);

                // Return the mapped DTO
                return _mapper.Map<PersonDetailsDto>(createdPerson);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating person: {FullName}", person.FullName);
                throw;
            }
        }
    }
}
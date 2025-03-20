using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Person.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Person.Queries.GetPersonById
{
    /// <summary>
    /// Handler for processing GetPersonByIdQuery requests.
    /// Implements the CQRS query handler pattern for retrieving person details by ID.
    /// </summary>
    public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, PersonDetailsDto>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPersonByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPersonByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="personRepository">The person repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetPersonByIdQueryHandler(
            IPersonRepository personRepository,
            IMapper mapper,
            ILogger<GetPersonByIdQueryHandler> logger)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPersonByIdQuery by retrieving a person entity from the database by its ID.
        /// </summary>
        /// <param name="request">The GetPersonByIdQuery containing the person ID to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A PersonDetailsDto representing the retrieved person, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<PersonDetailsDto> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving person with ID {PersonId} with all related entities", request.Id);

                // Retrieve the person with all related entities from the repository
                var person = await _personRepository.GetByIdWithRelatedEntitiesAsync(request.Id, cancellationToken);

                if (person == null)
                {
                    _logger.LogWarning("Person with ID {PersonId} not found", request.Id);
                    return new PersonDetailsDto();
                }

                // Map the entity to a DTO and return it
                var personDto = _mapper.Map<PersonDetailsDto>(person);

                _logger.LogInformation("Successfully retrieved person with ID: {PersonId} and all related entities", request.Id);
                return personDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving person with ID: {PersonId} and related entities", request.Id);
                throw;
            }
        }
    }
}
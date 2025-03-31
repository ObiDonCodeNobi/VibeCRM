using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Person;

namespace VibeCRM.Application.Features.Person.Queries.GetPersonWithRelatedEntities
{
    /// <summary>
    /// Handler for processing GetPersonWithRelatedEntitiesQuery requests.
    /// Implements the CQRS query handler pattern for retrieving a person with specified related entities.
    /// </summary>
    public class GetPersonWithRelatedEntitiesQueryHandler : IRequestHandler<GetPersonWithRelatedEntitiesQuery, PersonDetailsDto>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPersonWithRelatedEntitiesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPersonWithRelatedEntitiesQueryHandler"/> class.
        /// </summary>
        /// <param name="personRepository">The person repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetPersonWithRelatedEntitiesQueryHandler(
            IPersonRepository personRepository,
            IMapper mapper,
            ILogger<GetPersonWithRelatedEntitiesQueryHandler> logger)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPersonWithRelatedEntitiesQuery by retrieving a person entity from the database by its ID
        /// and loading the specified related entities.
        /// </summary>
        /// <param name="request">The GetPersonWithRelatedEntitiesQuery containing the person ID and related entities to load.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A PersonDetailsDto representing the retrieved person with related entities, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<PersonDetailsDto> Handle(GetPersonWithRelatedEntitiesQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving person with ID {PersonId} with specified related entities", request.Id);

                // Check if all related entities are requested
                bool loadAllEntities = request.LoadCompanies && request.LoadAddresses &&
                                      request.LoadPhoneNumbers && request.LoadEmailAddresses &&
                                      request.LoadActivities && request.LoadAttachments &&
                                      request.LoadNotes && request.LoadCalls;

                // If all entities are requested, use the optimized method
                if (loadAllEntities)
                {
                    var personWithAllEntities = await _personRepository.GetByIdWithRelatedEntitiesAsync(request.Id, cancellationToken);
                    if (personWithAllEntities == null)
                    {
                        _logger.LogWarning("Person with ID {PersonId} not found", request.Id);
                        return new PersonDetailsDto();
                    }

                    var personDto = _mapper.Map<PersonDetailsDto>(personWithAllEntities);
                    _logger.LogInformation("Successfully retrieved person with ID: {PersonId} and all related entities", request.Id);
                    return personDto;
                }

                // Otherwise, load only the requested entities
                var person = await _personRepository.GetByIdAsync(request.Id, cancellationToken);
                if (person == null)
                {
                    _logger.LogWarning("Person with ID {PersonId} not found", request.Id);
                    return new PersonDetailsDto();
                }

                // Load each requested related entity
                if (request.LoadCompanies)
                {
                    await _personRepository.LoadCompaniesAsync(person, cancellationToken);
                    _logger.LogDebug("Loaded companies for person with ID {PersonId}", request.Id);
                }

                if (request.LoadAddresses)
                {
                    await _personRepository.LoadAddressesAsync(person, cancellationToken);
                    _logger.LogDebug("Loaded addresses for person with ID {PersonId}", request.Id);
                }

                if (request.LoadPhoneNumbers)
                {
                    await _personRepository.LoadPhoneNumbersAsync(person, cancellationToken);
                    _logger.LogDebug("Loaded phone numbers for person with ID {PersonId}", request.Id);
                }

                if (request.LoadEmailAddresses)
                {
                    await _personRepository.LoadEmailAddressesAsync(person, cancellationToken);
                    _logger.LogDebug("Loaded email addresses for person with ID {PersonId}", request.Id);
                }

                if (request.LoadActivities)
                {
                    await _personRepository.LoadActivitiesAsync(person, cancellationToken);
                    _logger.LogDebug("Loaded activities for person with ID {PersonId}", request.Id);
                }

                if (request.LoadAttachments)
                {
                    await _personRepository.LoadAttachmentsAsync(person, cancellationToken);
                    _logger.LogDebug("Loaded attachments for person with ID {PersonId}", request.Id);
                }

                if (request.LoadNotes)
                {
                    await _personRepository.LoadNotesAsync(person, cancellationToken);
                    _logger.LogDebug("Loaded notes for person with ID {PersonId}", request.Id);
                }

                if (request.LoadCalls)
                {
                    await _personRepository.LoadCallsAsync(person, cancellationToken);
                    _logger.LogDebug("Loaded calls for person with ID {PersonId}", request.Id);
                }

                // Map the entity to a DTO and return it
                var personDetailsDto = _mapper.Map<PersonDetailsDto>(person);

                _logger.LogInformation("Successfully retrieved person with ID: {PersonId} and specified related entities", request.Id);
                return personDetailsDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving person with ID: {PersonId} and related entities", request.Id);
                throw;
            }
        }
    }
}
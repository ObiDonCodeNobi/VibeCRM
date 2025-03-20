using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Person.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Person.Queries.GetAllPersons
{
    /// <summary>
    /// Handler for processing GetAllPersonsQuery requests.
    /// Implements the CQRS query handler pattern for retrieving lists of persons.
    /// </summary>
    public class GetAllPersonsQueryHandler : IRequestHandler<GetAllPersonsQuery, IEnumerable<PersonListDto>>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllPersonsQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllPersonsQueryHandler"/> class.
        /// </summary>
        /// <param name="personRepository">The person repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public GetAllPersonsQueryHandler(
            IPersonRepository personRepository,
            IMapper mapper,
            ILogger<GetAllPersonsQueryHandler> logger)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllPersonsQuery by retrieving a list of person entities from the database.
        /// </summary>
        /// <param name="request">The GetAllPersonsQuery containing optional filtering and pagination parameters.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of PersonListDto objects representing the retrieved persons.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<IEnumerable<PersonListDto>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving persons with search term: {SearchTerm}, Page: {PageNumber}, PageSize: {PageSize}",
                    request.SearchTerm, request.PageNumber, request.PageSize);

                // Apply pagination and optional search filter
                IEnumerable<Domain.Entities.BusinessEntities.Person> persons;

                if (!string.IsNullOrEmpty(request.SearchTerm))
                {
                    // Use the search by name method if search term is provided
                    persons = await _personRepository.SearchByNameAsync(
                        request.SearchTerm,
                        cancellationToken);
                }
                else
                {
                    // Use the standard GetAllAsync method if no search term
                    persons = await _personRepository.GetAllAsync(cancellationToken);
                }

                // Apply pagination (this would typically be done in the repository, but we'll do it here for now)
                persons = persons
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList(); // Materialize the query before loading related entities

                if (persons == null || !persons.Any())
                {
                    _logger.LogInformation("No persons found matching the criteria. SearchTerm: {SearchTerm}, Page: {PageNumber}, PageSize: {PageSize}",
                        request.SearchTerm, request.PageNumber, request.PageSize);
                    return new List<PersonListDto>();
                }

                // Load primary related entities for each person to populate the list view
                foreach (var person in persons)
                {
                    // Load companies to determine primary company
                    await _personRepository.LoadCompaniesAsync(person, cancellationToken);
                    
                    // Load email addresses to determine primary email
                    await _personRepository.LoadEmailAddressesAsync(person, cancellationToken);
                    
                    // Load phone numbers to determine primary phone
                    await _personRepository.LoadPhoneNumbersAsync(person, cancellationToken);
                    
                    // Load addresses to determine primary address
                    await _personRepository.LoadAddressesAsync(person, cancellationToken);
                }

                // Map the entities to DTOs
                var personDtos = _mapper.Map<IEnumerable<PersonListDto>>(persons);

                // Set primary information for each person in the list
                foreach (var (personDto, person) in personDtos.Zip(persons, (dto, entity) => (dto, entity)))
                {
                    // Set primary company name
                    personDto.PrimaryCompanyName = person.Companies?.FirstOrDefault()?.Company?.Name ?? string.Empty;
                    
                    // Set primary email
                    personDto.PrimaryEmail = person.EmailAddresses?.FirstOrDefault()?.EmailAddress?.Address ?? string.Empty;
                    
                    // Set primary phone
                    personDto.PrimaryPhone = person.PhoneNumbers?.FirstOrDefault()?.Phone?.FormattedPhoneNumber ?? string.Empty;
                    
                    // Set primary address
                    var primaryAddress = person.Addresses?.FirstOrDefault()?.Address;
                    if (primaryAddress != null)
                    {
                        personDto.PrimaryAddress = $"{primaryAddress.Line1}, {primaryAddress.City}, " +
                                                  $"{(primaryAddress.State != null ? primaryAddress.State.Abbreviation : string.Empty)} {primaryAddress.Zip}";
                    }
                }

                _logger.LogInformation("Retrieved {Count} persons with related entities. SearchTerm: {SearchTerm}, Page: {PageNumber}, PageSize: {PageSize}",
                    persons.Count(), request.SearchTerm, request.PageNumber, request.PageSize);

                return personDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving persons with related entities. SearchTerm: {SearchTerm}, Page: {PageNumber}, PageSize: {PageSize}",
                    request.SearchTerm, request.PageNumber, request.PageSize);
                throw;
            }
        }
    }
}
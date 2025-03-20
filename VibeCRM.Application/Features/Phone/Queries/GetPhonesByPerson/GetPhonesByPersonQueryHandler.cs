using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.Phone.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Phone.Queries.GetPhonesByPerson
{
    /// <summary>
    /// Handler for processing GetPhonesByPersonQuery requests
    /// </summary>
    public class GetPhonesByPersonQueryHandler : IRequestHandler<GetPhonesByPersonQuery, IEnumerable<PhoneListDto>>
    {
        private readonly IPhoneRepository _phoneRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPhonesByPersonQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPhonesByPersonQueryHandler"/> class
        /// </summary>
        /// <param name="phoneRepository">The phone repository for database operations</param>
        /// <param name="personRepository">The person repository for verifying person existence</param>
        /// <param name="mapper">The AutoMapper instance for object mapping</param>
        /// <param name="logger">The logger for recording diagnostic information</param>
        public GetPhonesByPersonQueryHandler(
            IPhoneRepository phoneRepository,
            IPersonRepository personRepository,
            IMapper mapper,
            ILogger<GetPhonesByPersonQueryHandler> logger)
        {
            _phoneRepository = phoneRepository ?? throw new ArgumentNullException(nameof(phoneRepository));
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPhonesByPersonQuery request
        /// </summary>
        /// <param name="request">The query containing the person ID to retrieve phones for</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed</param>
        /// <returns>A collection of phone list DTOs associated with the specified person</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when the specified person does not exist</exception>
        public async Task<IEnumerable<PhoneListDto>> Handle(GetPhonesByPersonQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving phones for person with ID {PersonId}", request.PersonId);

                // Verify the person exists
                if (!await _personRepository.ExistsAsync(request.PersonId, cancellationToken))
                {
                    _logger.LogWarning("Person with ID {PersonId} not found", request.PersonId);
                    throw new InvalidOperationException($"Person with ID {request.PersonId} not found");
                }

                var phones = await _phoneRepository.GetByPersonAsync(request.PersonId, cancellationToken);

                _logger.LogInformation("Successfully retrieved phones for person with ID {PersonId}", request.PersonId);

                return _mapper.Map<IEnumerable<PhoneListDto>>(phones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving phones for person with ID {PersonId}", request.PersonId);
                throw;
            }
        }
    }
}
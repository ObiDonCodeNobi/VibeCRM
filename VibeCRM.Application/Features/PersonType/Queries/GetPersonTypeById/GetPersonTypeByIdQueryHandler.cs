using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.PersonType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PersonType.Queries.GetPersonTypeById
{
    /// <summary>
    /// Handler for the GetPersonTypeByIdQuery.
    /// Processes requests to retrieve a specific person type by its ID.
    /// </summary>
    public class GetPersonTypeByIdQueryHandler : IRequestHandler<GetPersonTypeByIdQuery, PersonTypeDetailsDto>
    {
        private readonly IPersonTypeRepository _personTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPersonTypeByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the GetPersonTypeByIdQueryHandler class.
        /// </summary>
        /// <param name="personTypeRepository">The repository for person type operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        public GetPersonTypeByIdQueryHandler(
            IPersonTypeRepository personTypeRepository,
            IMapper mapper,
            ILogger<GetPersonTypeByIdQueryHandler> logger)
        {
            _personTypeRepository = personTypeRepository ?? throw new ArgumentNullException(nameof(personTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPersonTypeByIdQuery by retrieving a person type by its ID from the database.
        /// </summary>
        /// <param name="request">The query containing the ID of the person type to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A PersonTypeDetailsDto representing the requested person type, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<PersonTypeDetailsDto> Handle(GetPersonTypeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving person type with ID: {PersonTypeId}", request.Id);

                var personType = await _personTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (personType == null)
                {
                    _logger.LogWarning("Person type with ID {PersonTypeId} not found", request.Id);
                    return new PersonTypeDetailsDto();
                }

                var personTypeDto = _mapper.Map<PersonTypeDetailsDto>(personType);

                _logger.LogInformation("Successfully retrieved person type with ID: {PersonTypeId}", request.Id);

                return personTypeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving person type with ID: {PersonTypeId}. Error: {ErrorMessage}",
                    request.Id, ex.Message);
                throw new InvalidOperationException($"Failed to retrieve person type: {ex.Message}", ex);
            }
        }
    }
}
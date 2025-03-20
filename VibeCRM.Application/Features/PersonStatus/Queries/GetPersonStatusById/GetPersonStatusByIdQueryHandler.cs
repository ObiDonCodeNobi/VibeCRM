using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.PersonStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PersonStatus.Queries.GetPersonStatusById
{
    /// <summary>
    /// Handler for processing GetPersonStatusByIdQuery requests.
    /// Implements the CQRS query handler pattern for retrieving a person status entity by ID.
    /// </summary>
    public class GetPersonStatusByIdQueryHandler : IRequestHandler<GetPersonStatusByIdQuery, PersonStatusDetailsDto>
    {
        private readonly IPersonStatusRepository _personStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPersonStatusByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPersonStatusByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="personStatusRepository">The person status repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public GetPersonStatusByIdQueryHandler(
            IPersonStatusRepository personStatusRepository,
            IMapper mapper,
            ILogger<GetPersonStatusByIdQueryHandler> logger)
        {
            _personStatusRepository = personStatusRepository ?? throw new ArgumentNullException(nameof(personStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPersonStatusByIdQuery by retrieving a person status by its ID from the database.
        /// </summary>
        /// <param name="request">The query containing the ID of the person status to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A PersonStatusDetailsDto representing the requested person status, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<PersonStatusDetailsDto> Handle(GetPersonStatusByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving person status with ID: {PersonStatusId}", request.Id);

                // Get the person status from the repository
                var personStatus = await _personStatusRepository.GetByIdAsync(request.Id, cancellationToken);
                if (personStatus == null)
                {
                    _logger.LogWarning("Person status with ID: {PersonStatusId} not found", request.Id);
                    return new PersonStatusDetailsDto();
                }

                // Map to DTO
                var personStatusDto = _mapper.Map<PersonStatusDetailsDto>(personStatus);

                // In a real implementation, we would need to get the count of people with this status
                // For now, we'll set it to 0
                personStatusDto.PeopleCount = 0;

                _logger.LogInformation("Successfully retrieved person status with ID: {PersonStatusId}", request.Id);

                return personStatusDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving person status with ID: {PersonStatusId}. Error: {ErrorMessage}",
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}
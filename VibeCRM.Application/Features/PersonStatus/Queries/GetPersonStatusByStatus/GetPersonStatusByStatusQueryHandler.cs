using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.PersonStatus;

namespace VibeCRM.Application.Features.PersonStatus.Queries.GetPersonStatusByStatus
{
    /// <summary>
    /// Handler for processing GetPersonStatusByStatusQuery requests.
    /// Implements the CQRS query handler pattern for retrieving person status entities by status name.
    /// </summary>
    public class GetPersonStatusByStatusQueryHandler : IRequestHandler<GetPersonStatusByStatusQuery, IEnumerable<PersonStatusListDto>>
    {
        private readonly IPersonStatusRepository _personStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPersonStatusByStatusQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPersonStatusByStatusQueryHandler"/> class.
        /// </summary>
        /// <param name="personStatusRepository">The person status repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public GetPersonStatusByStatusQueryHandler(
            IPersonStatusRepository personStatusRepository,
            IMapper mapper,
            ILogger<GetPersonStatusByStatusQueryHandler> logger)
        {
            _personStatusRepository = personStatusRepository ?? throw new ArgumentNullException(nameof(personStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPersonStatusByStatusQuery by retrieving person status entities by status name.
        /// </summary>
        /// <param name="request">The GetPersonStatusByStatusQuery containing the status name to search for.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of person status list DTOs if found; otherwise, an empty collection.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<IEnumerable<PersonStatusListDto>> Handle(GetPersonStatusByStatusQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving person statuses with status name: {Status}", request.Status);

                // Get the person statuses from the repository
                var personStatuses = await _personStatusRepository.GetByStatusAsync(request.Status, cancellationToken);
                if (personStatuses == null || !personStatuses.Any())
                {
                    _logger.LogWarning("No person statuses found with status name: {Status}", request.Status);
                    return Enumerable.Empty<PersonStatusListDto>();
                }

                // Map to DTOs
                var personStatusDtos = _mapper.Map<IEnumerable<PersonStatusListDto>>(personStatuses);

                // In a real implementation, we would need to get the count of people with each status
                // For now, we'll set them all to 0
                foreach (var dto in personStatusDtos)
                {
                    dto.PeopleCount = 0;
                }

                _logger.LogInformation("Successfully retrieved {Count} person statuses with status name: {Status}",
                    personStatusDtos.Count(), request.Status);

                return personStatusDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving person statuses with status name: {Status}. Error: {ErrorMessage}",
                    request.Status, ex.Message);
                throw;
            }
        }
    }
}
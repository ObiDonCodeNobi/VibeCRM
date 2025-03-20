using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.PersonStatus.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PersonStatus.Queries.GetPersonStatusByOrdinalPosition
{
    /// <summary>
    /// Handler for processing GetPersonStatusByOrdinalPositionQuery requests.
    /// Implements the CQRS query handler pattern for retrieving person statuses ordered by their ordinal position.
    /// </summary>
    public class GetPersonStatusByOrdinalPositionQueryHandler : IRequestHandler<GetPersonStatusByOrdinalPositionQuery, IEnumerable<PersonStatusListDto>>
    {
        private readonly IPersonStatusRepository _personStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPersonStatusByOrdinalPositionQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPersonStatusByOrdinalPositionQueryHandler"/> class.
        /// </summary>
        /// <param name="personStatusRepository">The person status repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public GetPersonStatusByOrdinalPositionQueryHandler(
            IPersonStatusRepository personStatusRepository,
            IMapper mapper,
            ILogger<GetPersonStatusByOrdinalPositionQueryHandler> logger)
        {
            _personStatusRepository = personStatusRepository ?? throw new ArgumentNullException(nameof(personStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPersonStatusByOrdinalPositionQuery by retrieving person statuses ordered by their ordinal position.
        /// </summary>
        /// <param name="request">The GetPersonStatusByOrdinalPositionQuery containing filter options.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of PersonStatusListDto objects ordered by their ordinal position.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<IEnumerable<PersonStatusListDto>> Handle(GetPersonStatusByOrdinalPositionQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving person statuses ordered by ordinal position. Include inactive: {IncludeInactive}", request.IncludeInactive);

                // Get person statuses ordered by ordinal position
                var personStatuses = await _personStatusRepository.GetByOrdinalPositionAsync(cancellationToken);

                // Filter by active status if needed
                if (!request.IncludeInactive)
                {
                    personStatuses = personStatuses.Where(p => p.Active);
                }

                // Map to DTOs
                var personStatusDtos = _mapper.Map<IEnumerable<PersonStatusListDto>>(personStatuses);

                // In a real implementation, we would need to get the count of people with each status
                // For now, we'll set them all to 0
                foreach (var dto in personStatusDtos)
                {
                    dto.PeopleCount = 0;
                }

                _logger.LogInformation("Successfully retrieved {Count} person statuses ordered by ordinal position", personStatusDtos.Count());

                return personStatusDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving person statuses ordered by ordinal position: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
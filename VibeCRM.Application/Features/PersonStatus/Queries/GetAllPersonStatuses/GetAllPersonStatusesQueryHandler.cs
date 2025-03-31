using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.PersonStatus;

namespace VibeCRM.Application.Features.PersonStatus.Queries.GetAllPersonStatuses
{
    /// <summary>
    /// Handler for processing GetAllPersonStatusesQuery requests.
    /// Implements the CQRS query handler pattern for retrieving all person statuses.
    /// </summary>
    public class GetAllPersonStatusesQueryHandler : IRequestHandler<GetAllPersonStatusesQuery, IEnumerable<PersonStatusListDto>>
    {
        private readonly IPersonStatusRepository _personStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllPersonStatusesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllPersonStatusesQueryHandler"/> class.
        /// </summary>
        /// <param name="personStatusRepository">The person status repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public GetAllPersonStatusesQueryHandler(
            IPersonStatusRepository personStatusRepository,
            IMapper mapper,
            ILogger<GetAllPersonStatusesQueryHandler> logger)
        {
            _personStatusRepository = personStatusRepository ?? throw new ArgumentNullException(nameof(personStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllPersonStatusesQuery by retrieving all person statuses from the database.
        /// </summary>
        /// <param name="request">The GetAllPersonStatusesQuery containing filter options.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of PersonStatusListDto objects representing all person statuses.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<IEnumerable<PersonStatusListDto>> Handle(GetAllPersonStatusesQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving all person statuses. Include inactive: {IncludeInactive}", request.IncludeInactive);

                // Get all person statuses
                var personStatuses = await _personStatusRepository.GetAllAsync(cancellationToken);

                // Filter by active status if needed
                if (!request.IncludeInactive)
                {
                    personStatuses = personStatuses.Where(p => p.Active);
                }

                // Order by ordinal position
                personStatuses = personStatuses.OrderBy(p => p.OrdinalPosition);

                // Map to DTOs
                var personStatusDtos = _mapper.Map<IEnumerable<PersonStatusListDto>>(personStatuses);

                // In a real implementation, we would need to get the count of people with each status
                // For now, we'll set them all to 0
                foreach (var dto in personStatusDtos)
                {
                    dto.PeopleCount = 0;
                }

                _logger.LogInformation("Successfully retrieved {Count} person statuses", personStatusDtos.Count());

                return personStatusDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all person statuses: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
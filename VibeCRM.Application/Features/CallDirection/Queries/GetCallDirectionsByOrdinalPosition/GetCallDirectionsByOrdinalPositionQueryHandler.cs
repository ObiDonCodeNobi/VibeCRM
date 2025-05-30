using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.CallDirection;

namespace VibeCRM.Application.Features.CallDirection.Queries.GetCallDirectionsByOrdinalPosition
{
    /// <summary>
    /// Handler for the GetCallDirectionsByOrdinalPositionQuery.
    /// Processes requests to retrieve all active call directions ordered by their ordinal position.
    /// </summary>
    public class GetCallDirectionsByOrdinalPositionQueryHandler : IRequestHandler<GetCallDirectionsByOrdinalPositionQuery, IEnumerable<CallDirectionListDto>>
    {
        private readonly ICallDirectionRepository _callDirectionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCallDirectionsByOrdinalPositionQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCallDirectionsByOrdinalPositionQueryHandler"/> class.
        /// </summary>
        /// <param name="callDirectionRepository">The call direction repository for data access.</param>
        /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        public GetCallDirectionsByOrdinalPositionQueryHandler(
            ICallDirectionRepository callDirectionRepository,
            IMapper mapper,
            ILogger<GetCallDirectionsByOrdinalPositionQueryHandler> logger)
        {
            _callDirectionRepository = callDirectionRepository ?? throw new ArgumentNullException(nameof(callDirectionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetCallDirectionsByOrdinalPositionQuery by retrieving all active call directions ordered by their ordinal position.
        /// </summary>
        /// <param name="request">The query to retrieve all active call directions ordered by ordinal position.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of call direction DTOs ordered by ordinal position.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        public async Task<IEnumerable<CallDirectionListDto>> Handle(GetCallDirectionsByOrdinalPositionQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Retrieving all active call directions ordered by ordinal position");

            var callDirections = await _callDirectionRepository.GetAllAsync(cancellationToken);
            var activeCallDirectionsOrdered = callDirections
                .Where(cd => cd.Active)
                .OrderBy(cd => cd.OrdinalPosition)
                .ToList();

            _logger.LogInformation("Retrieved {Count} active call directions ordered by ordinal position", activeCallDirectionsOrdered.Count);

            return _mapper.Map<IEnumerable<CallDirectionListDto>>(activeCallDirectionsOrdered);
        }
    }
}
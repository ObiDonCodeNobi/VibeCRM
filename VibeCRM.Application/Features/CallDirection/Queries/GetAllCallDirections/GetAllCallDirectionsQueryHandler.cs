using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.CallDirection.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.CallDirection.Queries.GetAllCallDirections
{
    /// <summary>
    /// Handler for the GetAllCallDirectionsQuery.
    /// Processes requests to retrieve all active call directions.
    /// </summary>
    public class GetAllCallDirectionsQueryHandler : IRequestHandler<GetAllCallDirectionsQuery, IEnumerable<CallDirectionListDto>>
    {
        private readonly ICallDirectionRepository _callDirectionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllCallDirectionsQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllCallDirectionsQueryHandler"/> class.
        /// </summary>
        /// <param name="callDirectionRepository">The call direction repository for data access.</param>
        /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        public GetAllCallDirectionsQueryHandler(
            ICallDirectionRepository callDirectionRepository,
            IMapper mapper,
            ILogger<GetAllCallDirectionsQueryHandler> logger)
        {
            _callDirectionRepository = callDirectionRepository ?? throw new ArgumentNullException(nameof(callDirectionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllCallDirectionsQuery by retrieving all active call directions from the database.
        /// </summary>
        /// <param name="request">The query to retrieve all active call directions.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of call direction DTOs.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        public async Task<IEnumerable<CallDirectionListDto>> Handle(GetAllCallDirectionsQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Retrieving all active call directions");

            var callDirections = await _callDirectionRepository.GetAllAsync(cancellationToken);
            var activeCallDirections = callDirections.Where(cd => cd.Active).ToList();

            _logger.LogInformation("Retrieved {Count} active call directions", activeCallDirections.Count);

            return _mapper.Map<IEnumerable<CallDirectionListDto>>(activeCallDirections);
        }
    }
}
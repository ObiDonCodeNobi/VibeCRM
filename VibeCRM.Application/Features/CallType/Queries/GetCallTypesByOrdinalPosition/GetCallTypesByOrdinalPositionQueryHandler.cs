using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.CallType;

namespace VibeCRM.Application.Features.CallType.Queries.GetCallTypesByOrdinalPosition
{
    /// <summary>
    /// Handler for processing the GetCallTypesByOrdinalPositionQuery.
    /// Implements IRequestHandler to handle the query and return a collection of CallTypeListDto.
    /// </summary>
    public class GetCallTypesByOrdinalPositionQueryHandler : IRequestHandler<GetCallTypesByOrdinalPositionQuery, IEnumerable<CallTypeListDto>>
    {
        private readonly ICallTypeRepository _callTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCallTypesByOrdinalPositionQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCallTypesByOrdinalPositionQueryHandler"/> class.
        /// </summary>
        /// <param name="callTypeRepository">The call type repository for data access operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters is null.</exception>
        public GetCallTypesByOrdinalPositionQueryHandler(
            ICallTypeRepository callTypeRepository,
            IMapper mapper,
            ILogger<GetCallTypesByOrdinalPositionQueryHandler> logger)
        {
            _callTypeRepository = callTypeRepository ?? throw new ArgumentNullException(nameof(callTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetCallTypesByOrdinalPositionQuery by retrieving call types ordered by their ordinal position.
        /// </summary>
        /// <param name="request">The GetCallTypesByOrdinalPositionQuery request.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of CallTypeListDto representing call types ordered by their ordinal position.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the call types could not be retrieved.</exception>
        public async Task<IEnumerable<CallTypeListDto>> Handle(GetCallTypesByOrdinalPositionQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Retrieving call types ordered by ordinal position");

            try
            {
                // Get call types ordered by ordinal position
                var callTypes = await _callTypeRepository.GetByOrdinalPositionAsync(cancellationToken);

                // Map the entities to DTOs and return them
                return _mapper.Map<IEnumerable<CallTypeListDto>>(callTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving call types ordered by ordinal position");
                throw new InvalidOperationException("Failed to retrieve call types ordered by ordinal position", ex);
            }
        }
    }
}
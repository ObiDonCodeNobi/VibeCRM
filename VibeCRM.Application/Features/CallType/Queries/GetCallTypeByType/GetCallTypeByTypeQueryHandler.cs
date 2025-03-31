using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.CallType;

namespace VibeCRM.Application.Features.CallType.Queries.GetCallTypeByType
{
    /// <summary>
    /// Handler for processing the GetCallTypeByTypeQuery.
    /// Implements IRequestHandler to handle the query and return a collection of CallTypeListDto.
    /// </summary>
    public class GetCallTypeByTypeQueryHandler : IRequestHandler<GetCallTypeByTypeQuery, IEnumerable<CallTypeListDto>>
    {
        private readonly ICallTypeRepository _callTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCallTypeByTypeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCallTypeByTypeQueryHandler"/> class.
        /// </summary>
        /// <param name="callTypeRepository">The call type repository for data access operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters is null.</exception>
        public GetCallTypeByTypeQueryHandler(
            ICallTypeRepository callTypeRepository,
            IMapper mapper,
            ILogger<GetCallTypeByTypeQueryHandler> logger)
        {
            _callTypeRepository = callTypeRepository ?? throw new ArgumentNullException(nameof(callTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetCallTypeByTypeQuery by retrieving call types with the specified type name from the database.
        /// </summary>
        /// <param name="request">The GetCallTypeByTypeQuery containing the type name to search for.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of CallTypeListDto representing the matching call types.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the call types could not be retrieved.</exception>
        public async Task<IEnumerable<CallTypeListDto>> Handle(GetCallTypeByTypeQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Retrieving call types with type: {Type}", request.Type);

            try
            {
                // Get call types by type name
                var callTypes = await _callTypeRepository.GetByTypeAsync(request.Type, cancellationToken);

                // Map the entities to DTOs and return them
                return _mapper.Map<IEnumerable<CallTypeListDto>>(callTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving call types with type: {Type}", request.Type);
                throw new InvalidOperationException($"Failed to retrieve call types with type: {request.Type}", ex);
            }
        }
    }
}
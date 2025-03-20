using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.CallType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.CallType.Queries.GetAllCallTypes
{
    /// <summary>
    /// Handler for processing the GetAllCallTypesQuery.
    /// Implements IRequestHandler to handle the query and return a collection of CallTypeListDto.
    /// </summary>
    public class GetAllCallTypesQueryHandler : IRequestHandler<GetAllCallTypesQuery, IEnumerable<CallTypeListDto>>
    {
        private readonly ICallTypeRepository _callTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllCallTypesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllCallTypesQueryHandler"/> class.
        /// </summary>
        /// <param name="callTypeRepository">The call type repository for data access operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters is null.</exception>
        public GetAllCallTypesQueryHandler(
            ICallTypeRepository callTypeRepository,
            IMapper mapper,
            ILogger<GetAllCallTypesQueryHandler> logger)
        {
            _callTypeRepository = callTypeRepository ?? throw new ArgumentNullException(nameof(callTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllCallTypesQuery by retrieving all active call types from the database.
        /// </summary>
        /// <param name="request">The GetAllCallTypesQuery request.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of CallTypeListDto representing all active call types.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the call types could not be retrieved.</exception>
        public async Task<IEnumerable<CallTypeListDto>> Handle(GetAllCallTypesQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Retrieving all active call types");

            try
            {
                // Get all active call types
                var callTypes = await _callTypeRepository.GetAllAsync(cancellationToken);

                // Map the entities to DTOs and return them
                return _mapper.Map<IEnumerable<CallTypeListDto>>(callTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all active call types");
                throw new InvalidOperationException("Failed to retrieve all active call types", ex);
            }
        }
    }
}
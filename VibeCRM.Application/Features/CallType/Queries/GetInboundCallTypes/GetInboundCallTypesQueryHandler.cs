using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.CallType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.CallType.Queries.GetInboundCallTypes
{
    /// <summary>
    /// Handler for processing the GetInboundCallTypesQuery.
    /// Implements IRequestHandler to handle the query and return a collection of CallTypeListDto.
    /// </summary>
    public class GetInboundCallTypesQueryHandler : IRequestHandler<GetInboundCallTypesQuery, IEnumerable<CallTypeListDto>>
    {
        private readonly ICallTypeRepository _callTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetInboundCallTypesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetInboundCallTypesQueryHandler"/> class.
        /// </summary>
        /// <param name="callTypeRepository">The call type repository for data access operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters is null.</exception>
        public GetInboundCallTypesQueryHandler(
            ICallTypeRepository callTypeRepository,
            IMapper mapper,
            ILogger<GetInboundCallTypesQueryHandler> logger)
        {
            _callTypeRepository = callTypeRepository ?? throw new ArgumentNullException(nameof(callTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetInboundCallTypesQuery by retrieving all inbound call types from the database.
        /// </summary>
        /// <param name="request">The GetInboundCallTypesQuery request.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of CallTypeListDto representing inbound call types.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the inbound call types could not be retrieved.</exception>
        public async Task<IEnumerable<CallTypeListDto>> Handle(GetInboundCallTypesQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Retrieving inbound call types");

            try
            {
                // Get inbound call types
                var inboundCallTypes = await _callTypeRepository.GetInboundTypesAsync(cancellationToken);

                // Map the entities to DTOs and return them
                return _mapper.Map<IEnumerable<CallTypeListDto>>(inboundCallTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving inbound call types");
                throw new InvalidOperationException("Failed to retrieve inbound call types", ex);
            }
        }
    }
}
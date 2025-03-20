using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.CallType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.CallType.Queries.GetOutboundCallTypes
{
    /// <summary>
    /// Handler for processing the GetOutboundCallTypesQuery.
    /// Implements IRequestHandler to handle the query and return a collection of CallTypeListDto.
    /// </summary>
    public class GetOutboundCallTypesQueryHandler : IRequestHandler<GetOutboundCallTypesQuery, IEnumerable<CallTypeListDto>>
    {
        private readonly ICallTypeRepository _callTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetOutboundCallTypesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetOutboundCallTypesQueryHandler"/> class.
        /// </summary>
        /// <param name="callTypeRepository">The call type repository for data access operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters is null.</exception>
        public GetOutboundCallTypesQueryHandler(
            ICallTypeRepository callTypeRepository,
            IMapper mapper,
            ILogger<GetOutboundCallTypesQueryHandler> logger)
        {
            _callTypeRepository = callTypeRepository ?? throw new ArgumentNullException(nameof(callTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetOutboundCallTypesQuery by retrieving all outbound call types from the database.
        /// </summary>
        /// <param name="request">The GetOutboundCallTypesQuery request.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of CallTypeListDto representing outbound call types.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the outbound call types could not be retrieved.</exception>
        public async Task<IEnumerable<CallTypeListDto>> Handle(GetOutboundCallTypesQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Retrieving outbound call types");

            try
            {
                // Get outbound call types
                var outboundCallTypes = await _callTypeRepository.GetOutboundTypesAsync(cancellationToken);

                // Map the entities to DTOs and return them
                return _mapper.Map<IEnumerable<CallTypeListDto>>(outboundCallTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving outbound call types");
                throw new InvalidOperationException("Failed to retrieve outbound call types", ex);
            }
        }
    }
}
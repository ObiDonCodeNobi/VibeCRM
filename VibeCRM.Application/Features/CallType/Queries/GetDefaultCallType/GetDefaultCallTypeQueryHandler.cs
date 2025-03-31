using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Common.Exceptions;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.CallType;

namespace VibeCRM.Application.Features.CallType.Queries.GetDefaultCallType
{
    /// <summary>
    /// Handler for processing the GetDefaultCallTypeQuery.
    /// Implements IRequestHandler to handle the query and return a CallTypeDto.
    /// </summary>
    public class GetDefaultCallTypeQueryHandler : IRequestHandler<GetDefaultCallTypeQuery, CallTypeDto>
    {
        private readonly ICallTypeRepository _callTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDefaultCallTypeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultCallTypeQueryHandler"/> class.
        /// </summary>
        /// <param name="callTypeRepository">The call type repository for data access operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters is null.</exception>
        public GetDefaultCallTypeQueryHandler(
            ICallTypeRepository callTypeRepository,
            IMapper mapper,
            ILogger<GetDefaultCallTypeQueryHandler> logger)
        {
            _callTypeRepository = callTypeRepository ?? throw new ArgumentNullException(nameof(callTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetDefaultCallTypeQuery by retrieving the default call type from the database.
        /// </summary>
        /// <param name="request">The GetDefaultCallTypeQuery request.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A CallTypeDto representing the default call type, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the default call type could not be retrieved.</exception>
        public async Task<CallTypeDto> Handle(GetDefaultCallTypeQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Retrieving default call type");

            try
            {
                // Get the default call type
                var defaultCallType = await _callTypeRepository.GetDefaultAsync(cancellationToken);
                if (defaultCallType == null)
                {
                    _logger.LogWarning("Default call type not found");
                    throw new NotFoundException(nameof(Domain.Entities.TypeStatusEntities.CallDirection), "default");
                }

                // Map the entity to a DTO and return it
                return _mapper.Map<CallTypeDto>(defaultCallType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving default call type");
                throw new InvalidOperationException("Failed to retrieve default call type", ex);
            }
        }
    }
}
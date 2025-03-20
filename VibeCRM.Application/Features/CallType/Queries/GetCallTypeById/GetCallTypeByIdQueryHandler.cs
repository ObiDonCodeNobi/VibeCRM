using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Common.Exceptions;
using VibeCRM.Application.Features.CallType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.CallType.Queries.GetCallTypeById
{
    /// <summary>
    /// Handler for processing the GetCallTypeByIdQuery.
    /// Implements IRequestHandler to handle the query and return a CallTypeDetailsDto.
    /// </summary>
    public class GetCallTypeByIdQueryHandler : IRequestHandler<GetCallTypeByIdQuery, CallTypeDetailsDto>
    {
        private readonly ICallTypeRepository _callTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCallTypeByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCallTypeByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="callTypeRepository">The call type repository for data access operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters is null.</exception>
        public GetCallTypeByIdQueryHandler(
            ICallTypeRepository callTypeRepository,
            IMapper mapper,
            ILogger<GetCallTypeByIdQueryHandler> logger)
        {
            _callTypeRepository = callTypeRepository ?? throw new ArgumentNullException(nameof(callTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetCallTypeByIdQuery by retrieving a call type by its ID from the database.
        /// </summary>
        /// <param name="request">The GetCallTypeByIdQuery containing the ID of the call type to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A CallTypeDetailsDto representing the requested call type, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the call type could not be retrieved.</exception>
        public async Task<CallTypeDetailsDto> Handle(GetCallTypeByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Retrieving call type with ID: {Id}", request.Id);

            try
            {
                // Get the call type by ID
                var callType = await _callTypeRepository.GetByIdAsync(request.Id, cancellationToken);
                if (callType == null)
                {
                    _logger.LogWarning("Call type with ID {CallTypeId} not found", request.Id);
                    throw new NotFoundException(nameof(Domain.Entities.TypeStatusEntities.CallDirection), request.Id);
                }

                // Map the entity to a DTO and return it
                return _mapper.Map<CallTypeDetailsDto>(callType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving call type with ID: {Id}", request.Id);
                throw new InvalidOperationException($"Failed to retrieve call type with ID: {request.Id}", ex);
            }
        }
    }
}
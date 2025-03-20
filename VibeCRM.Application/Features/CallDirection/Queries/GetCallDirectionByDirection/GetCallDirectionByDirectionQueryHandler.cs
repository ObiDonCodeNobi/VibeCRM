using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.CallDirection.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Application.Common.Exceptions;

namespace VibeCRM.Application.Features.CallDirection.Queries.GetCallDirectionByDirection
{
    /// <summary>
    /// Handler for the GetCallDirectionByDirectionQuery.
    /// Processes requests to retrieve a call direction by its direction name.
    /// </summary>
    public class GetCallDirectionByDirectionQueryHandler : IRequestHandler<GetCallDirectionByDirectionQuery, CallDirectionDetailsDto>
    {
        private readonly ICallDirectionRepository _callDirectionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCallDirectionByDirectionQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCallDirectionByDirectionQueryHandler"/> class.
        /// </summary>
        /// <param name="callDirectionRepository">The call direction repository for data access.</param>
        /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        public GetCallDirectionByDirectionQueryHandler(
            ICallDirectionRepository callDirectionRepository,
            IMapper mapper,
            ILogger<GetCallDirectionByDirectionQueryHandler> logger)
        {
            _callDirectionRepository = callDirectionRepository ?? throw new ArgumentNullException(nameof(callDirectionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetCallDirectionByDirectionQuery by retrieving a call direction by its direction name from the database.
        /// </summary>
        /// <param name="request">The query containing the direction name of the call direction to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>The call direction details DTO if found, otherwise null.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        public async Task<CallDirectionDetailsDto> Handle(GetCallDirectionByDirectionQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Retrieving call direction with direction name: {Direction}", request.Direction);

            var callDirections = await _callDirectionRepository.GetByDirectionAsync(request.Direction, cancellationToken);
            var activeCallDirection = callDirections.FirstOrDefault(cd => cd.Active);

            if (activeCallDirection == null)
            {
                _logger.LogWarning("Call direction with direction name: {Direction} not found or inactive", request.Direction);
                throw new NotFoundException(nameof(Domain.Entities.TypeStatusEntities.CallDirection), request.Direction);
            }

            _logger.LogInformation("Successfully retrieved call direction with direction name: {Direction}", request.Direction);

            return _mapper.Map<CallDirectionDetailsDto>(activeCallDirection);
        }
    }
}
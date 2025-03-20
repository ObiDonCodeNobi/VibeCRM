using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Common.Exceptions;
using VibeCRM.Application.Features.CallDirection.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.CallDirection.Queries.GetDefaultCallDirection
{
    /// <summary>
    /// Handler for the GetDefaultCallDirectionQuery.
    /// Processes requests to retrieve the default call direction.
    /// </summary>
    public class GetDefaultCallDirectionQueryHandler : IRequestHandler<GetDefaultCallDirectionQuery, CallDirectionDto>
    {
        private readonly ICallDirectionRepository _callDirectionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDefaultCallDirectionQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultCallDirectionQueryHandler"/> class.
        /// </summary>
        /// <param name="callDirectionRepository">The call direction repository for data access.</param>
        /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        public GetDefaultCallDirectionQueryHandler(
            ICallDirectionRepository callDirectionRepository,
            IMapper mapper,
            ILogger<GetDefaultCallDirectionQueryHandler> logger)
        {
            _callDirectionRepository = callDirectionRepository ?? throw new ArgumentNullException(nameof(callDirectionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetDefaultCallDirectionQuery by retrieving the default call direction from the database.
        /// </summary>
        /// <param name="request">The query to retrieve the default call direction.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>The default call direction DTO if found, otherwise null.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        public async Task<CallDirectionDto> Handle(GetDefaultCallDirectionQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Retrieving default call direction");

            var defaultCallDirection = await _callDirectionRepository.GetDefaultAsync(cancellationToken);
            if (defaultCallDirection == null)
            {
                _logger.LogWarning("Default call direction not found");
                throw new NotFoundException(nameof(Domain.Entities.TypeStatusEntities.CallDirection), "default");
            }
            else if (!defaultCallDirection.Active)
            {
                _logger.LogWarning("Default call direction not found or inactive");
                return new CallDirectionDto();
            }

            _logger.LogInformation("Successfully retrieved default call direction with ID: {Id}", defaultCallDirection.Id);

            return _mapper.Map<CallDirectionDto>(defaultCallDirection);
        }
    }
}
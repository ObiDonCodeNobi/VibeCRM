using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.CallDirection.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.CallDirection.Queries.GetCallDirectionById
{
    /// <summary>
    /// Handler for the GetCallDirectionByIdQuery.
    /// Processes requests to retrieve a call direction by its unique identifier.
    /// </summary>
    public class GetCallDirectionByIdQueryHandler : IRequestHandler<GetCallDirectionByIdQuery, CallDirectionDetailsDto?>
    {
        private readonly ICallDirectionRepository _callDirectionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCallDirectionByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCallDirectionByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="callDirectionRepository">The call direction repository for data access.</param>
        /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        public GetCallDirectionByIdQueryHandler(
            ICallDirectionRepository callDirectionRepository,
            IMapper mapper,
            ILogger<GetCallDirectionByIdQueryHandler> logger)
        {
            _callDirectionRepository = callDirectionRepository ?? throw new ArgumentNullException(nameof(callDirectionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetCallDirectionByIdQuery by retrieving a call direction by its ID from the database.
        /// </summary>
        /// <param name="request">The query containing the ID of the call direction to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>The call direction details DTO if found, otherwise null.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        public async Task<CallDirectionDetailsDto?> Handle(GetCallDirectionByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Retrieving call direction with ID: {Id}", request.Id);

            var callDirection = await _callDirectionRepository.GetByIdAsync(request.Id, cancellationToken);
            if (callDirection == null || !callDirection.Active)
            {
                _logger.LogWarning("Call direction with ID: {Id} not found or inactive", request.Id);
                return null;
            }

            _logger.LogInformation("Successfully retrieved call direction with ID: {Id}", request.Id);

            return _mapper.Map<CallDirectionDetailsDto>(callDirection);
        }
    }
}
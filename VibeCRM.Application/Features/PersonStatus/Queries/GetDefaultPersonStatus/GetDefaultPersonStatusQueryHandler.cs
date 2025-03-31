using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.PersonStatus;

namespace VibeCRM.Application.Features.PersonStatus.Queries.GetDefaultPersonStatus
{
    /// <summary>
    /// Handler for processing GetDefaultPersonStatusQuery requests.
    /// Implements the CQRS query handler pattern for retrieving the default person status entity.
    /// </summary>
    public class GetDefaultPersonStatusQueryHandler : IRequestHandler<GetDefaultPersonStatusQuery, PersonStatusDto>
    {
        private readonly IPersonStatusRepository _personStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDefaultPersonStatusQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDefaultPersonStatusQueryHandler"/> class.
        /// </summary>
        /// <param name="personStatusRepository">The person status repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
        public GetDefaultPersonStatusQueryHandler(
            IPersonStatusRepository personStatusRepository,
            IMapper mapper,
            ILogger<GetDefaultPersonStatusQueryHandler> logger)
        {
            _personStatusRepository = personStatusRepository ?? throw new ArgumentNullException(nameof(personStatusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetDefaultPersonStatusQuery by retrieving the default person status from the database.
        /// </summary>
        /// <param name="request">The query to retrieve the default person status.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>The default person status DTO if found, otherwise null.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        public async Task<PersonStatusDto> Handle(GetDefaultPersonStatusQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            try
            {
                _logger.LogInformation("Retrieving default person status");

                // Get the default person status from the repository
                var personStatus = await _personStatusRepository.GetDefaultAsync(cancellationToken);
                if (personStatus == null)
                {
                    _logger.LogWarning("No default person status found");
                    return new PersonStatusDto();
                }

                // Map to DTO
                var personStatusDto = _mapper.Map<PersonStatusDto>(personStatus);

                _logger.LogInformation("Successfully retrieved default person status with ID: {PersonStatusId}", personStatusDto.Id);

                return personStatusDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving default person status: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
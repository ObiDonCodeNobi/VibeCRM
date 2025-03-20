using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.PersonType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PersonType.Queries.GetAllPersonTypes
{
    /// <summary>
    /// Handler for the GetAllPersonTypesQuery.
    /// Processes requests to retrieve all active person types from the system.
    /// </summary>
    public class GetAllPersonTypesQueryHandler : IRequestHandler<GetAllPersonTypesQuery, IEnumerable<PersonTypeListDto>>
    {
        private readonly IPersonTypeRepository _personTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllPersonTypesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the GetAllPersonTypesQueryHandler class.
        /// </summary>
        /// <param name="personTypeRepository">The repository for person type operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        public GetAllPersonTypesQueryHandler(
            IPersonTypeRepository personTypeRepository,
            IMapper mapper,
            ILogger<GetAllPersonTypesQueryHandler> logger)
        {
            _personTypeRepository = personTypeRepository ?? throw new ArgumentNullException(nameof(personTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllPersonTypesQuery by retrieving all active person types from the repository.
        /// </summary>
        /// <param name="request">The query to retrieve all person types.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of all active person types mapped to DTOs.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the person types could not be retrieved.</exception>
        public async Task<IEnumerable<PersonTypeListDto>> Handle(GetAllPersonTypesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving all person types");

                var personTypes = await _personTypeRepository.GetAllAsync(cancellationToken);
                var personTypeDtos = _mapper.Map<IEnumerable<PersonTypeListDto>>(personTypes);

                _logger.LogInformation("Successfully retrieved {Count} person types", personTypes.Count());

                return personTypeDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all person types. Error: {ErrorMessage}", ex.Message);
                throw new InvalidOperationException($"Failed to retrieve person types: {ex.Message}", ex);
            }
        }
    }
}
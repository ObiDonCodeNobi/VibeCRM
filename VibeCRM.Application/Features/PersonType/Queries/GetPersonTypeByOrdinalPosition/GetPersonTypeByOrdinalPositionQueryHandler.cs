using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VibeCRM.Application.Features.PersonType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.PersonType.Queries.GetPersonTypeByOrdinalPosition
{
    /// <summary>
    /// Handler for the GetPersonTypeByOrdinalPositionQuery.
    /// Processes requests to retrieve person types ordered by their ordinal position.
    /// </summary>
    public class GetPersonTypeByOrdinalPositionQueryHandler : IRequestHandler<GetPersonTypeByOrdinalPositionQuery, IEnumerable<PersonTypeListDto>>
    {
        private readonly IPersonTypeRepository _personTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPersonTypeByOrdinalPositionQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the GetPersonTypeByOrdinalPositionQueryHandler class.
        /// </summary>
        /// <param name="personTypeRepository">The repository for person type operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for diagnostic information.</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        public GetPersonTypeByOrdinalPositionQueryHandler(
            IPersonTypeRepository personTypeRepository,
            IMapper mapper,
            ILogger<GetPersonTypeByOrdinalPositionQueryHandler> logger)
        {
            _personTypeRepository = personTypeRepository ?? throw new ArgumentNullException(nameof(personTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetPersonTypeByOrdinalPositionQuery by retrieving person types ordered by their ordinal position.
        /// </summary>
        /// <param name="request">The query to retrieve ordered person types.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of person types ordered by their ordinal position, mapped to DTOs.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the person types could not be retrieved.</exception>
        public async Task<IEnumerable<PersonTypeListDto>> Handle(GetPersonTypeByOrdinalPositionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving person types ordered by ordinal position");

                var personTypes = await _personTypeRepository.GetByOrdinalPositionAsync(cancellationToken);
                var personTypesCollection = personTypes.ToList();
                var personTypeDtos = _mapper.Map<IEnumerable<PersonTypeListDto>>(personTypesCollection);

                _logger.LogInformation("Successfully retrieved {Count} person types ordered by ordinal position", personTypesCollection.Count);

                return personTypeDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving person types ordered by ordinal position. Error: {ErrorMessage}", ex.Message);
                throw new InvalidOperationException($"Failed to retrieve person types ordered by ordinal position: {ex.Message}", ex);
            }
        }
    }
}

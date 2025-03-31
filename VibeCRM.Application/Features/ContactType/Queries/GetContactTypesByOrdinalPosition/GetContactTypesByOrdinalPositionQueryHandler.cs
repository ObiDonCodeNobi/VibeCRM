using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.ContactType;

namespace VibeCRM.Application.Features.ContactType.Queries.GetContactTypesByOrdinalPosition
{
    /// <summary>
    /// Handler for the GetContactTypesByOrdinalPositionQuery.
    /// Retrieves contact types ordered by their ordinal position.
    /// </summary>
    public class GetContactTypesByOrdinalPositionQueryHandler : IRequestHandler<GetContactTypesByOrdinalPositionQuery, IEnumerable<ContactTypeListDto>>
    {
        private readonly IContactTypeRepository _contactTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetContactTypesByOrdinalPositionQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetContactTypesByOrdinalPositionQueryHandler"/> class.
        /// </summary>
        /// <param name="contactTypeRepository">The contact type repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="logger">The logger.</param>
        public GetContactTypesByOrdinalPositionQueryHandler(
            IContactTypeRepository contactTypeRepository,
            IMapper mapper,
            ILogger<GetContactTypesByOrdinalPositionQueryHandler> logger)
        {
            _contactTypeRepository = contactTypeRepository ?? throw new ArgumentNullException(nameof(contactTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetContactTypesByOrdinalPositionQuery by retrieving contact types ordered by their ordinal position.
        /// </summary>
        /// <param name="request">The query to retrieve contact types ordered by ordinal position.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A collection of contact type list DTOs ordered by ordinal position.</returns>
        public async Task<IEnumerable<ContactTypeListDto>> Handle(GetContactTypesByOrdinalPositionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving contact types ordered by ordinal position");

                // Get contact types from repository ordered by ordinal position
                var contactTypes = await _contactTypeRepository.GetByOrdinalPositionAsync(cancellationToken);

                // Map to DTOs
                var contactTypeDtos = _mapper.Map<IEnumerable<ContactTypeListDto>>(contactTypes);

                // For each DTO, we would typically set the ContactCount property
                // This would be implemented in the repository or a separate service
                // For now, we'll set it to 0 as a placeholder

                _logger.LogInformation("Successfully retrieved contact types ordered by ordinal position");

                return contactTypeDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving contact types ordered by ordinal position: {ErrorMessage}", ex.Message);
                throw;
            }
        }
    }
}
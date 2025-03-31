using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.AttachmentType;

namespace VibeCRM.Application.Features.AttachmentType.Queries.GetAttachmentTypeByOrdinalPosition
{
    /// <summary>
    /// Handler for the GetAttachmentTypeByOrdinalPositionQuery.
    /// Processes requests to retrieve attachment types ordered by their ordinal position.
    /// </summary>
    public class GetAttachmentTypeByOrdinalPositionQueryHandler : IRequestHandler<GetAttachmentTypeByOrdinalPositionQuery, IEnumerable<AttachmentTypeListDto>>
    {
        private readonly IAttachmentTypeRepository _attachmentTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAttachmentTypeByOrdinalPositionQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAttachmentTypeByOrdinalPositionQueryHandler"/> class.
        /// </summary>
        /// <param name="attachmentTypeRepository">The attachment type repository for data access.</param>
        /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        public GetAttachmentTypeByOrdinalPositionQueryHandler(
            IAttachmentTypeRepository attachmentTypeRepository,
            IMapper mapper,
            ILogger<GetAttachmentTypeByOrdinalPositionQueryHandler> logger)
        {
            _attachmentTypeRepository = attachmentTypeRepository ?? throw new ArgumentNullException(nameof(attachmentTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAttachmentTypeByOrdinalPositionQuery by retrieving attachment types ordered by their ordinal position.
        /// </summary>
        /// <param name="request">The query to retrieve attachment types ordered by ordinal position.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of attachment type DTOs ordered by ordinal position.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        public async Task<IEnumerable<AttachmentTypeListDto>> Handle(GetAttachmentTypeByOrdinalPositionQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Retrieving attachment types ordered by ordinal position");

            var attachmentTypes = await _attachmentTypeRepository.GetByOrdinalPositionAsync(cancellationToken);
            var activeAttachmentTypes = attachmentTypes.Where(at => at.Active).ToList();

            _logger.LogInformation("Retrieved {Count} active attachment types ordered by ordinal position", activeAttachmentTypes.Count);

            return _mapper.Map<IEnumerable<AttachmentTypeListDto>>(activeAttachmentTypes);
        }
    }
}
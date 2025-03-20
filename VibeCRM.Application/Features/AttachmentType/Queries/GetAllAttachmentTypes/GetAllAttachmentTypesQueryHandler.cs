using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.AttachmentType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.AttachmentType.Queries.GetAllAttachmentTypes
{
    /// <summary>
    /// Handler for the GetAllAttachmentTypesQuery.
    /// Processes requests to retrieve all active attachment types.
    /// </summary>
    public class GetAllAttachmentTypesQueryHandler : IRequestHandler<GetAllAttachmentTypesQuery, IEnumerable<AttachmentTypeListDto>>
    {
        private readonly IAttachmentTypeRepository _attachmentTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllAttachmentTypesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllAttachmentTypesQueryHandler"/> class.
        /// </summary>
        /// <param name="attachmentTypeRepository">The attachment type repository for data access.</param>
        /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        public GetAllAttachmentTypesQueryHandler(
            IAttachmentTypeRepository attachmentTypeRepository,
            IMapper mapper,
            ILogger<GetAllAttachmentTypesQueryHandler> logger)
        {
            _attachmentTypeRepository = attachmentTypeRepository ?? throw new ArgumentNullException(nameof(attachmentTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAllAttachmentTypesQuery by retrieving all active attachment types from the database.
        /// </summary>
        /// <param name="request">The query to retrieve all attachment types.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of attachment type DTOs.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        public async Task<IEnumerable<AttachmentTypeListDto>> Handle(GetAllAttachmentTypesQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Retrieving all active attachment types");

            var attachmentTypes = await _attachmentTypeRepository.GetAllAsync(cancellationToken);
            var activeAttachmentTypes = attachmentTypes.Where(at => at.Active).ToList();

            _logger.LogInformation("Retrieved {Count} active attachment types", activeAttachmentTypes.Count);

            return _mapper.Map<IEnumerable<AttachmentTypeListDto>>(activeAttachmentTypes);
        }
    }
}
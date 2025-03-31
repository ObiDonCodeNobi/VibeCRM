using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;
using VibeCRM.Shared.DTOs.AttachmentType;

namespace VibeCRM.Application.Features.AttachmentType.Queries.GetAttachmentTypeByFileExtension
{
    /// <summary>
    /// Handler for the GetAttachmentTypeByFileExtensionQuery.
    /// Processes requests to retrieve attachment types that support a specific file extension.
    /// </summary>
    public class GetAttachmentTypeByFileExtensionQueryHandler : IRequestHandler<GetAttachmentTypeByFileExtensionQuery, IEnumerable<AttachmentTypeListDto>>
    {
        private readonly IAttachmentTypeRepository _attachmentTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAttachmentTypeByFileExtensionQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAttachmentTypeByFileExtensionQueryHandler"/> class.
        /// </summary>
        /// <param name="attachmentTypeRepository">The attachment type repository for data access.</param>
        /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        public GetAttachmentTypeByFileExtensionQueryHandler(
            IAttachmentTypeRepository attachmentTypeRepository,
            IMapper mapper,
            ILogger<GetAttachmentTypeByFileExtensionQueryHandler> logger)
        {
            _attachmentTypeRepository = attachmentTypeRepository ?? throw new ArgumentNullException(nameof(attachmentTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAttachmentTypeByFileExtensionQuery by retrieving attachment types that support the specified file extension.
        /// </summary>
        /// <param name="request">The query containing the file extension to search for.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A collection of attachment type DTOs that support the specified file extension.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        public async Task<IEnumerable<AttachmentTypeListDto>> Handle(GetAttachmentTypeByFileExtensionQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Retrieving attachment types that support file extension: {FileExtension}", request.FileExtension);

            var attachmentTypes = await _attachmentTypeRepository.GetByFileExtensionAsync(request.FileExtension, cancellationToken);
            var activeAttachmentTypes = attachmentTypes.Where(at => at.Active).ToList();

            _logger.LogInformation("Retrieved {Count} active attachment types that support file extension: {FileExtension}",
                activeAttachmentTypes.Count, request.FileExtension);

            return _mapper.Map<IEnumerable<AttachmentTypeListDto>>(activeAttachmentTypes);
        }
    }
}
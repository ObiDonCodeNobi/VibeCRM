using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.AttachmentType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.AttachmentType.Queries.GetAttachmentTypeByType
{
    /// <summary>
    /// Handler for the GetAttachmentTypeByTypeQuery.
    /// Processes requests to retrieve an attachment type by its type name.
    /// </summary>
    public class GetAttachmentTypeByTypeQueryHandler : IRequestHandler<GetAttachmentTypeByTypeQuery, AttachmentTypeDetailsDto?>
    {
        private readonly IAttachmentTypeRepository _attachmentTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAttachmentTypeByTypeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAttachmentTypeByTypeQueryHandler"/> class.
        /// </summary>
        /// <param name="attachmentTypeRepository">The attachment type repository for data access.</param>
        /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        public GetAttachmentTypeByTypeQueryHandler(
            IAttachmentTypeRepository attachmentTypeRepository,
            IMapper mapper,
            ILogger<GetAttachmentTypeByTypeQueryHandler> logger)
        {
            _attachmentTypeRepository = attachmentTypeRepository ?? throw new ArgumentNullException(nameof(attachmentTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAttachmentTypeByTypeQuery by retrieving an attachment type by its type name from the database.
        /// </summary>
        /// <param name="request">The query containing the type name of the attachment type to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>The attachment type details DTO if found, otherwise null.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        public async Task<AttachmentTypeDetailsDto?> Handle(GetAttachmentTypeByTypeQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Retrieving attachment type with type name: {Type}", request.Type);

            var attachmentTypes = await _attachmentTypeRepository.GetByTypeAsync(request.Type, cancellationToken);
            var attachmentType = attachmentTypes.FirstOrDefault(at => at.Active);

            if (attachmentType == null)
            {
                _logger.LogWarning("Attachment type with type name: {Type} not found or inactive", request.Type);
                return null;
            }

            _logger.LogInformation("Successfully retrieved attachment type with type name: {Type}", request.Type);

            return _mapper.Map<AttachmentTypeDetailsDto>(attachmentType);
        }
    }
}
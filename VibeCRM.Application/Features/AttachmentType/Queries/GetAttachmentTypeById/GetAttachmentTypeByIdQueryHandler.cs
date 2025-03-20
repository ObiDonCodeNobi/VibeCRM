using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Application.Features.AttachmentType.DTOs;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.AttachmentType.Queries.GetAttachmentTypeById
{
    /// <summary>
    /// Handler for the GetAttachmentTypeByIdQuery.
    /// Processes requests to retrieve an attachment type by its unique identifier.
    /// </summary>
    public class GetAttachmentTypeByIdQueryHandler : IRequestHandler<GetAttachmentTypeByIdQuery, AttachmentTypeDetailsDto?>
    {
        private readonly IAttachmentTypeRepository _attachmentTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAttachmentTypeByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAttachmentTypeByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="attachmentTypeRepository">The attachment type repository for data access.</param>
        /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        public GetAttachmentTypeByIdQueryHandler(
            IAttachmentTypeRepository attachmentTypeRepository,
            IMapper mapper,
            ILogger<GetAttachmentTypeByIdQueryHandler> logger)
        {
            _attachmentTypeRepository = attachmentTypeRepository ?? throw new ArgumentNullException(nameof(attachmentTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the GetAttachmentTypeByIdQuery by retrieving an attachment type by its ID from the database.
        /// </summary>
        /// <param name="request">The query containing the ID of the attachment type to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>The attachment type details DTO if found, otherwise null.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        public async Task<AttachmentTypeDetailsDto?> Handle(GetAttachmentTypeByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Retrieving attachment type with ID: {Id}", request.Id);

            var attachmentType = await _attachmentTypeRepository.GetByIdAsync(request.Id, cancellationToken);
            if (attachmentType == null || !attachmentType.Active)
            {
                _logger.LogWarning("Attachment type with ID: {Id} not found or inactive", request.Id);
                return null;
            }

            _logger.LogInformation("Successfully retrieved attachment type with ID: {Id}", request.Id);

            return _mapper.Map<AttachmentTypeDetailsDto>(attachmentType);
        }
    }
}
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Attachment;

namespace VibeCRM.Application.Features.Attachment.Commands.CreateAttachment
{
    /// <summary>
    /// Handler for processing CreateAttachmentCommand requests.
    /// Implements the CQRS command handler pattern for creating new attachment entities.
    /// </summary>
    public class CreateAttachmentCommandHandler : IRequestHandler<CreateAttachmentCommand, AttachmentDetailsDto>
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateAttachmentCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAttachmentCommandHandler"/> class.
        /// </summary>
        /// <param name="attachmentRepository">The attachment repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public CreateAttachmentCommandHandler(
            IAttachmentRepository attachmentRepository,
            IMapper mapper,
            ILogger<CreateAttachmentCommandHandler> logger)
        {
            _attachmentRepository = attachmentRepository ?? throw new ArgumentNullException(nameof(attachmentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateAttachmentCommand by creating a new attachment entity in the database.
        /// </summary>
        /// <param name="request">The CreateAttachmentCommand containing the attachment details.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>An AttachmentDetailsDto representing the newly created attachment.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        public async Task<AttachmentDetailsDto> Handle(CreateAttachmentCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            // Map command to entity
            var attachment = _mapper.Map<VibeCRM.Domain.Entities.BusinessEntities.Attachment>(request);

            try
            {
                // Add the attachment to the repository
                var createdAttachment = await _attachmentRepository.AddAsync(attachment, cancellationToken);
                _logger.LogInformation("Created new attachment with ID: {AttachmentId}", createdAttachment.AttachmentId);

                // Return the mapped DTO
                return _mapper.Map<AttachmentDetailsDto>(createdAttachment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating attachment: {Subject}", request.Subject);
                throw;
            }
        }
    }
}
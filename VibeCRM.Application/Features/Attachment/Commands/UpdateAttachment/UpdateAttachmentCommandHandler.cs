using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;
using VibeCRM.Shared.DTOs.Attachment;

namespace VibeCRM.Application.Features.Attachment.Commands.UpdateAttachment
{
    /// <summary>
    /// Handler for processing UpdateAttachmentCommand requests.
    /// Implements the CQRS command handler pattern for updating existing attachment entities.
    /// </summary>
    public class UpdateAttachmentCommandHandler : IRequestHandler<UpdateAttachmentCommand, AttachmentDetailsDto>
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAttachmentCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAttachmentCommandHandler"/> class.
        /// </summary>
        /// <param name="attachmentRepository">The attachment repository for database operations.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public UpdateAttachmentCommandHandler(
            IAttachmentRepository attachmentRepository,
            IMapper mapper,
            ILogger<UpdateAttachmentCommandHandler> logger)
        {
            _attachmentRepository = attachmentRepository ?? throw new ArgumentNullException(nameof(attachmentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateAttachmentCommand by updating an existing attachment entity in the database.
        /// </summary>
        /// <param name="request">The UpdateAttachmentCommand containing the updated attachment details.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>An AttachmentDetailsDto representing the updated attachment.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the attachment ID is empty.</exception>
        public async Task<AttachmentDetailsDto> Handle(UpdateAttachmentCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Id == Guid.Empty) throw new ArgumentException("Attachment ID cannot be empty", nameof(request.Id));

            try
            {
                // Get the existing attachment (Active=1 filter is applied in the repository)
                var existingAttachment = await _attachmentRepository.GetByIdAsync(request.Id, cancellationToken);

                if (existingAttachment == null)
                {
                    _logger.LogWarning("Attachment with ID {AttachmentId} not found or is inactive", request.Id);
                    throw new InvalidOperationException($"Attachment with ID {request.Id} not found or is inactive");
                }

                // Map updated properties while preserving existing ones
                _mapper.Map(request, existingAttachment);

                // Update the attachment in the repository
                var updatedAttachment = await _attachmentRepository.UpdateAsync(existingAttachment, cancellationToken);
                _logger.LogInformation("Updated attachment with ID: {AttachmentId}", updatedAttachment.AttachmentId);

                // Return the mapped DTO
                return _mapper.Map<AttachmentDetailsDto>(updatedAttachment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating attachment with ID {AttachmentId}: {ErrorMessage}",
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.Business;

namespace VibeCRM.Application.Features.Attachment.Commands.DeleteAttachment
{
    /// <summary>
    /// Handler for processing DeleteAttachmentCommand requests.
    /// Implements the CQRS command handler pattern for soft-deleting attachment entities.
    /// </summary>
    public class DeleteAttachmentCommandHandler : IRequestHandler<DeleteAttachmentCommand, bool>
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly ILogger<DeleteAttachmentCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAttachmentCommandHandler"/> class.
        /// </summary>
        /// <param name="attachmentRepository">The attachment repository for database operations.</param>
        /// <param name="logger">The logger for capturing diagnostic information.</param>
        public DeleteAttachmentCommandHandler(
            IAttachmentRepository attachmentRepository,
            ILogger<DeleteAttachmentCommandHandler> logger)
        {
            _attachmentRepository = attachmentRepository ?? throw new ArgumentNullException(nameof(attachmentRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteAttachmentCommand by soft-deleting an existing attachment entity in the database.
        /// </summary>
        /// <param name="request">The DeleteAttachmentCommand containing the attachment ID to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the attachment ID is empty.</exception>
        public async Task<bool> Handle(DeleteAttachmentCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Id == Guid.Empty) throw new ArgumentException("Attachment ID cannot be empty", nameof(request.Id));

            try
            {
                // Get the existing attachment (Active=1 filter is applied in the repository)
                var existingAttachment = await _attachmentRepository.GetByIdAsync(request.Id, cancellationToken);

                if (existingAttachment == null)
                {
                    _logger.LogWarning("Attachment with ID {AttachmentId} not found or is already inactive", request.Id);
                    return false;
                }

                // Update the modified by information before deletion
                existingAttachment.ModifiedBy = request.ModifiedBy;
                existingAttachment.ModifiedDate = DateTime.UtcNow;

                // First update the entity to save the modified by information
                await _attachmentRepository.UpdateAsync(existingAttachment, cancellationToken);

                // Then soft delete the attachment by ID (sets Active = 0)
                var result = await _attachmentRepository.DeleteAsync(request.Id, cancellationToken);

                if (result)
                {
                    _logger.LogInformation("Soft-deleted attachment with ID: {AttachmentId}", request.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to soft-delete attachment with ID: {AttachmentId}", request.Id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting attachment with ID {AttachmentId}: {ErrorMessage}",
                    request.Id, ex.Message);
                throw;
            }
        }
    }
}
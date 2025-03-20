using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.AttachmentType.Commands.DeleteAttachmentType
{
    /// <summary>
    /// Handler for the DeleteAttachmentTypeCommand.
    /// Processes requests to soft delete an existing attachment type by setting its Active property to false.
    /// </summary>
    public class DeleteAttachmentTypeCommandHandler : IRequestHandler<DeleteAttachmentTypeCommand, bool>
    {
        private readonly IAttachmentTypeRepository _attachmentTypeRepository;
        private readonly ILogger<DeleteAttachmentTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAttachmentTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="attachmentTypeRepository">The attachment type repository for data access.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when attachmentTypeRepository or logger is null.</exception>
        public DeleteAttachmentTypeCommandHandler(
            IAttachmentTypeRepository attachmentTypeRepository,
            ILogger<DeleteAttachmentTypeCommandHandler> logger)
        {
            _attachmentTypeRepository = attachmentTypeRepository ?? throw new ArgumentNullException(nameof(attachmentTypeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the DeleteAttachmentTypeCommand by soft deleting an existing attachment type in the database.
        /// </summary>
        /// <param name="request">The command containing the ID of the attachment type to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>True if the deletion was successful, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        public async Task<bool> Handle(DeleteAttachmentTypeCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Soft deleting attachment type with ID: {Id}", request.Id);

            var existingEntity = await _attachmentTypeRepository.GetByIdAsync(request.Id, cancellationToken);
            if (existingEntity == null)
            {
                _logger.LogWarning("Attachment type with ID: {Id} not found", request.Id);
                return false;
            }

            // Update the entity to mark it as inactive (soft delete)
            existingEntity.Active = false;
            existingEntity.ModifiedBy = Guid.Parse(request.ModifiedBy);
            existingEntity.ModifiedDate = DateTime.UtcNow;

            await _attachmentTypeRepository.UpdateAsync(existingEntity, cancellationToken);

            _logger.LogInformation("Successfully soft deleted attachment type with ID: {Id}", request.Id);

            return true;
        }
    }
}
using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.AttachmentType.Commands.UpdateAttachmentType
{
    /// <summary>
    /// Handler for the UpdateAttachmentTypeCommand.
    /// Processes requests to update an existing attachment type.
    /// </summary>
    public class UpdateAttachmentTypeCommandHandler : IRequestHandler<UpdateAttachmentTypeCommand, bool>
    {
        private readonly IAttachmentTypeRepository _attachmentTypeRepository;
        private readonly ILogger<UpdateAttachmentTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAttachmentTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="attachmentTypeRepository">The attachment type repository for data access.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when attachmentTypeRepository or logger is null.</exception>
        public UpdateAttachmentTypeCommandHandler(
            IAttachmentTypeRepository attachmentTypeRepository,
            ILogger<UpdateAttachmentTypeCommandHandler> logger)
        {
            _attachmentTypeRepository = attachmentTypeRepository ?? throw new ArgumentNullException(nameof(attachmentTypeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the UpdateAttachmentTypeCommand by updating an existing attachment type in the database.
        /// </summary>
        /// <param name="request">The command containing the attachment type details to update.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>True if the update was successful, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        public async Task<bool> Handle(UpdateAttachmentTypeCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Updating attachment type with ID: {Id}", request.Id);

            var existingEntity = await _attachmentTypeRepository.GetByIdAsync(request.Id, cancellationToken);
            if (existingEntity == null)
            {
                _logger.LogWarning("Attachment type with ID: {Id} not found", request.Id);
                return false;
            }

            // Update entity properties
            existingEntity.Type = request.Type;
            existingEntity.Description = request.Description;
            existingEntity.OrdinalPosition = request.OrdinalPosition;
            existingEntity.ModifiedBy = Guid.Parse(request.ModifiedBy);
            existingEntity.ModifiedDate = DateTime.UtcNow;

            await _attachmentTypeRepository.UpdateAsync(existingEntity, cancellationToken);

            _logger.LogInformation("Successfully updated attachment type with ID: {Id}", request.Id);

            return true;
        }
    }
}
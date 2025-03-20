using MediatR;
using Microsoft.Extensions.Logging;
using VibeCRM.Domain.Interfaces.Repositories.TypeStatus;

namespace VibeCRM.Application.Features.AttachmentType.Commands.CreateAttachmentType
{
    /// <summary>
    /// Handler for the CreateAttachmentTypeCommand.
    /// Processes requests to create a new attachment type.
    /// </summary>
    public class CreateAttachmentTypeCommandHandler : IRequestHandler<CreateAttachmentTypeCommand, Guid>
    {
        private readonly IAttachmentTypeRepository _attachmentTypeRepository;
        private readonly ILogger<CreateAttachmentTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAttachmentTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="attachmentTypeRepository">The attachment type repository for data access.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when attachmentTypeRepository or logger is null.</exception>
        public CreateAttachmentTypeCommandHandler(
            IAttachmentTypeRepository attachmentTypeRepository,
            ILogger<CreateAttachmentTypeCommandHandler> logger)
        {
            _attachmentTypeRepository = attachmentTypeRepository ?? throw new ArgumentNullException(nameof(attachmentTypeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the CreateAttachmentTypeCommand by creating a new attachment type in the database.
        /// </summary>
        /// <param name="request">The command containing the attachment type details to create.</param>
        /// <param name="cancellationToken">A token to cancel the operation if needed.</param>
        /// <returns>The ID of the newly created attachment type.</returns>
        /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
        public async Task<Guid> Handle(CreateAttachmentTypeCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogInformation("Creating new attachment type: {Type}", request.Type);

            var entity = new Domain.Entities.TypeStatusEntities.AttachmentType
            {
                Id = Guid.NewGuid(),
                Type = request.Type,
                Description = request.Description,
                OrdinalPosition = request.OrdinalPosition,
                CreatedBy = Guid.Parse(request.CreatedBy),
                CreatedDate = DateTime.UtcNow,
                ModifiedBy = Guid.Parse(request.CreatedBy),
                ModifiedDate = DateTime.UtcNow,
                Active = true
            };

            var result = await _attachmentTypeRepository.AddAsync(entity, cancellationToken);

            _logger.LogInformation("Successfully created attachment type with ID: {Id}", result.Id);

            return result.Id;
        }
    }
}
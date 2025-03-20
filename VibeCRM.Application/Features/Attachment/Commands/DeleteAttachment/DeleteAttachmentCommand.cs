using MediatR;

namespace VibeCRM.Application.Features.Attachment.Commands.DeleteAttachment
{
    /// <summary>
    /// Command to delete (soft delete) an existing attachment in the system.
    /// This is used in the CQRS pattern as the request object for attachment deletion.
    /// </summary>
    public class DeleteAttachmentCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the attachment to delete.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user performing the deletion.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAttachmentCommand"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the attachment to delete.</param>
        /// <param name="modifiedBy">The identifier of the user performing the deletion.</param>
        public DeleteAttachmentCommand(Guid id, Guid modifiedBy)
        {
            Id = id;
            ModifiedBy = modifiedBy;
        }
    }
}
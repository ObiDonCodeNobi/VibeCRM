using MediatR;

namespace VibeCRM.Application.Features.AttachmentType.Commands.DeleteAttachmentType
{
    /// <summary>
    /// Command for soft deleting an existing attachment type by setting its Active property to false.
    /// </summary>
    public class DeleteAttachmentTypeCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the attachment type to delete.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the user who is deleting the attachment type.
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;
    }
}
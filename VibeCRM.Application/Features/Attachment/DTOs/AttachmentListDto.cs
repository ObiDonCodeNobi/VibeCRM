namespace VibeCRM.Application.Features.Attachment.DTOs
{
    /// <summary>
    /// List Data Transfer Object for Attachment entities.
    /// Contains properties needed for displaying attachments in lists.
    /// </summary>
    public class AttachmentListDto : AttachmentDto
    {
        /// <summary>
        /// Gets or sets the name of the attachment type.
        /// </summary>
        public string AttachmentTypeName { get; set; } = string.Empty;
    }
}
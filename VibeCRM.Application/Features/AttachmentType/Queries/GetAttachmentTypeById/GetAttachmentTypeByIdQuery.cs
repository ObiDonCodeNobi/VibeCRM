using MediatR;
using VibeCRM.Shared.DTOs.AttachmentType;

namespace VibeCRM.Application.Features.AttachmentType.Queries.GetAttachmentTypeById
{
    /// <summary>
    /// Query to retrieve an attachment type by its unique identifier.
    /// </summary>
    public class GetAttachmentTypeByIdQuery : IRequest<AttachmentTypeDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the attachment type to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}
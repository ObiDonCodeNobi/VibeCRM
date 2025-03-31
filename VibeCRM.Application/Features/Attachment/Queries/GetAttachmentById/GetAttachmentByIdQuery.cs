using MediatR;
using VibeCRM.Shared.DTOs.Attachment;

namespace VibeCRM.Application.Features.Attachment.Queries.GetAttachmentById
{
    /// <summary>
    /// Query to retrieve a specific attachment by its unique identifier.
    /// This is used in the CQRS pattern as the request object for fetching a single attachment.
    /// </summary>
    public class GetAttachmentByIdQuery : IRequest<AttachmentDetailsDto?>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the attachment to retrieve.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAttachmentByIdQuery"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the attachment to retrieve.</param>
        public GetAttachmentByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
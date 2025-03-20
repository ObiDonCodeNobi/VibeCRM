using MediatR;
using VibeCRM.Application.Features.AttachmentType.DTOs;

namespace VibeCRM.Application.Features.AttachmentType.Queries.GetAttachmentTypeByOrdinalPosition
{
    /// <summary>
    /// Query to retrieve attachment types ordered by their ordinal position.
    /// </summary>
    public class GetAttachmentTypeByOrdinalPositionQuery : IRequest<IEnumerable<AttachmentTypeListDto>>
    {
        // This query doesn't require any parameters as it retrieves all attachment types ordered by ordinal position
    }
}
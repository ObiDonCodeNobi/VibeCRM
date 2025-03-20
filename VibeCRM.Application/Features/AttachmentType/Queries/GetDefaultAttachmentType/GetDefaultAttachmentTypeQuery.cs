using MediatR;
using VibeCRM.Application.Features.AttachmentType.DTOs;

namespace VibeCRM.Application.Features.AttachmentType.Queries.GetDefaultAttachmentType
{
    /// <summary>
    /// Query to retrieve the default attachment type (typically the one with the lowest ordinal position).
    /// </summary>
    public class GetDefaultAttachmentTypeQuery : IRequest<AttachmentTypeDetailsDto>
    {
        // This query doesn't require any parameters as it retrieves the default attachment type
    }
}
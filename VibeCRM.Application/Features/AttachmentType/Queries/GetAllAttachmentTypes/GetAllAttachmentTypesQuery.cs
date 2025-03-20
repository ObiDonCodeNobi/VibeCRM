using MediatR;
using VibeCRM.Application.Features.AttachmentType.DTOs;

namespace VibeCRM.Application.Features.AttachmentType.Queries.GetAllAttachmentTypes
{
    /// <summary>
    /// Query to retrieve all active attachment types.
    /// </summary>
    public class GetAllAttachmentTypesQuery : IRequest<IEnumerable<AttachmentTypeListDto>>
    {
        // This query doesn't require any parameters as it retrieves all active attachment types
    }
}
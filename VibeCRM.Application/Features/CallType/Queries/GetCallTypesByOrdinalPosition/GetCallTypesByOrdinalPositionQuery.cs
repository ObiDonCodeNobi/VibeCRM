using MediatR;
using VibeCRM.Application.Features.CallType.DTOs;

namespace VibeCRM.Application.Features.CallType.Queries.GetCallTypesByOrdinalPosition
{
    /// <summary>
    /// Query for retrieving call types ordered by their ordinal position.
    /// Implements IRequest to return a collection of CallTypeListDto.
    /// </summary>
    public class GetCallTypesByOrdinalPositionQuery : IRequest<IEnumerable<CallTypeListDto>>
    {
        // No parameters needed for this query
    }
}
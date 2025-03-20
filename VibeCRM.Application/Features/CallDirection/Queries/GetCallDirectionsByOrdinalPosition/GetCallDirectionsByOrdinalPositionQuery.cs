using MediatR;
using VibeCRM.Application.Features.CallDirection.DTOs;

namespace VibeCRM.Application.Features.CallDirection.Queries.GetCallDirectionsByOrdinalPosition
{
    /// <summary>
    /// Query to retrieve all active call directions ordered by their ordinal position.
    /// </summary>
    public class GetCallDirectionsByOrdinalPositionQuery : IRequest<IEnumerable<CallDirectionListDto>>
    {
        // This query doesn't require any parameters as it retrieves all active call directions ordered by ordinal position
    }
}
using MediatR;
using VibeCRM.Application.Features.ActivityStatus.DTOs;

namespace VibeCRM.Application.Features.ActivityStatus.Queries.GetActivityStatusByOrdinalPosition
{
    /// <summary>
    /// Query for retrieving activity statuses ordered by ordinal position.
    /// </summary>
    public class GetActivityStatusByOrdinalPositionQuery : IRequest<IEnumerable<ActivityStatusListDto>>
    {
        // This is a parameter-less query
    }
}
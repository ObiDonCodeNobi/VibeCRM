using MediatR;
using VibeCRM.Application.Features.ActivityType.DTOs;

namespace VibeCRM.Application.Features.ActivityType.Queries.GetAllActivityTypes
{
    /// <summary>
    /// Query for retrieving all activity types.
    /// </summary>
    public class GetAllActivityTypesQuery : IRequest<IEnumerable<ActivityTypeListDto>>
    {
        // No parameters needed for this query
    }
}
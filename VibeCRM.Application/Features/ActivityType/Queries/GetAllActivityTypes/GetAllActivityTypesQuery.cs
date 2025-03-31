using MediatR;
using VibeCRM.Shared.DTOs.ActivityType;

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
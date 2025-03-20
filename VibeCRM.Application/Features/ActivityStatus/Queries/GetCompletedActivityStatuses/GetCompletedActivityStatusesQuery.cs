using MediatR;
using VibeCRM.Application.Features.ActivityStatus.DTOs;

namespace VibeCRM.Application.Features.ActivityStatus.Queries.GetCompletedActivityStatuses
{
    /// <summary>
    /// Query for retrieving all completed activity statuses.
    /// </summary>
    public class GetCompletedActivityStatusesQuery : IRequest<IEnumerable<ActivityStatusListDto>>
    {
        // This is a parameter-less query
    }
}
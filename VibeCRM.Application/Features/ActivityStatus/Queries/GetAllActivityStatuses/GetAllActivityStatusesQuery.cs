using MediatR;
using VibeCRM.Shared.DTOs.ActivityStatus;

namespace VibeCRM.Application.Features.ActivityStatus.Queries.GetAllActivityStatuses
{
    /// <summary>
    /// Query for retrieving all activity statuses.
    /// </summary>
    public class GetAllActivityStatusesQuery : IRequest<IEnumerable<ActivityStatusListDto>>
    {
        // This is a parameter-less query
    }
}
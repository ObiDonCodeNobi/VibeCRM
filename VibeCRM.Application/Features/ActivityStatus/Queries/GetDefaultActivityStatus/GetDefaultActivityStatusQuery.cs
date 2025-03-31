using MediatR;
using VibeCRM.Shared.DTOs.ActivityStatus;

namespace VibeCRM.Application.Features.ActivityStatus.Queries.GetDefaultActivityStatus
{
    /// <summary>
    /// Query for retrieving the default activity status.
    /// </summary>
    public class GetDefaultActivityStatusQuery : IRequest<ActivityStatusDto>
    {
        // This is a parameter-less query
    }
}
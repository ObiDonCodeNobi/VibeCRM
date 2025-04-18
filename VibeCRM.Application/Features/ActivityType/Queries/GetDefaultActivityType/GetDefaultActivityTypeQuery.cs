using MediatR;
using VibeCRM.Shared.DTOs.ActivityType;

namespace VibeCRM.Application.Features.ActivityType.Queries.GetDefaultActivityType
{
    /// <summary>
    /// Query for retrieving the default activity type.
    /// The default is typically the one with the lowest ordinal position.
    /// </summary>
    public class GetDefaultActivityTypeQuery : IRequest<ActivityTypeDto>
    {
        // No parameters needed for this query
    }
}
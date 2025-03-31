using MediatR;
using VibeCRM.Shared.DTOs.ActivityType;

namespace VibeCRM.Application.Features.ActivityType.Queries.GetActivityTypeByOrdinalPosition
{
    /// <summary>
    /// Query for retrieving an activity type by its ordinal position.
    /// </summary>
    public class GetActivityTypeByOrdinalPositionQuery : IRequest<ActivityTypeDto>
    {
        /// <summary>
        /// Gets or sets the ordinal position to search for.
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}
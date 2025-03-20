using MediatR;
using VibeCRM.Application.Features.ActivityType.DTOs;

namespace VibeCRM.Application.Features.ActivityType.Queries.GetActivityTypeByType
{
    /// <summary>
    /// Query for retrieving an activity type by its type name.
    /// </summary>
    public class GetActivityTypeByTypeQuery : IRequest<ActivityTypeDetailsDto>
    {
        /// <summary>
        /// Gets or sets the type name to search for.
        /// </summary>
        public string Type { get; set; } = string.Empty;
    }
}
using MediatR;
using VibeCRM.Application.Features.ActivityType.DTOs;

namespace VibeCRM.Application.Features.ActivityType.Queries.GetActivityTypeById
{
    /// <summary>
    /// Query for retrieving an activity type by its ID.
    /// </summary>
    public class GetActivityTypeByIdQuery : IRequest<ActivityTypeDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the activity type to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}
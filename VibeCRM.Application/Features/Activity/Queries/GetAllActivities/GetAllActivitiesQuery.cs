using MediatR;
using VibeCRM.Application.Features.Activity.DTOs;

namespace VibeCRM.Application.Features.Activity.Queries.GetAllActivities
{
    /// <summary>
    /// Query to retrieve all active activities in the system.
    /// This is used in the CQRS pattern as the request object for retrieving a list of activities.
    /// Only returns activities where Active = true (not soft-deleted).
    /// </summary>
    public class GetAllActivitiesQuery : IRequest<List<ActivityListDto>>
    {
        /// <summary>
        /// Gets or sets the page number for pagination (1-based).
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Gets or sets the page size for pagination.
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
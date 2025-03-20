using MediatR;
using VibeCRM.Application.Features.ActivityStatus.DTOs;

namespace VibeCRM.Application.Features.ActivityStatus.Queries.GetActivityStatusByStatus
{
    /// <summary>
    /// Query for retrieving activity statuses by status name.
    /// </summary>
    public class GetActivityStatusByStatusQuery : IRequest<IEnumerable<ActivityStatusListDto>>
    {
        /// <summary>
        /// Gets or sets the status name to search for.
        /// </summary>
        public string Status { get; set; } = string.Empty;
    }
}
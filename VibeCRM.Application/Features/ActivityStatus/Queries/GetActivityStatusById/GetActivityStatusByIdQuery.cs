using MediatR;
using VibeCRM.Application.Features.ActivityStatus.DTOs;

namespace VibeCRM.Application.Features.ActivityStatus.Queries.GetActivityStatusById
{
    /// <summary>
    /// Query for retrieving an activity status by its ID.
    /// </summary>
    public class GetActivityStatusByIdQuery : IRequest<ActivityStatusDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the activity status to retrieve.
        /// </summary>
        public Guid Id { get; set; }
    }
}
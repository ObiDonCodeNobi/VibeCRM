using MediatR;
using VibeCRM.Application.Features.Activity.DTOs;

namespace VibeCRM.Application.Features.Activity.Queries.GetActivityById
{
    /// <summary>
    /// Query to retrieve a specific activity by its unique identifier.
    /// This is used in the CQRS pattern as the request object for retrieving activity details.
    /// </summary>
    public class GetActivityByIdQuery : IRequest<ActivityDetailsDto?>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the activity to retrieve.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetActivityByIdQuery"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the activity to retrieve.</param>
        public GetActivityByIdQuery(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetActivityByIdQuery"/> class.
        /// This parameterless constructor is required for certain serialization scenarios.
        /// </summary>
        public GetActivityByIdQuery()
        {
        }
    }
}
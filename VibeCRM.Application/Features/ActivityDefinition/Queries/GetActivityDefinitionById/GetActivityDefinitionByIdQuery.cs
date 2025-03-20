using MediatR;
using VibeCRM.Application.Features.ActivityDefinition.DTOs;

namespace VibeCRM.Application.Features.ActivityDefinition.Queries.GetActivityDefinitionById
{
    /// <summary>
    /// Query for retrieving a specific activity definition by its unique identifier.
    /// Implements the CQRS query pattern for retrieving a single entity.
    /// </summary>
    public class GetActivityDefinitionByIdQuery : IRequest<ActivityDefinitionDetailsDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the activity definition to retrieve.
        /// </summary>
        public Guid ActivityDefinitionId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetActivityDefinitionByIdQuery"/> class.
        /// </summary>
        public GetActivityDefinitionByIdQuery()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetActivityDefinitionByIdQuery"/> class with a specific activity definition ID.
        /// </summary>
        /// <param name="activityDefinitionId">The unique identifier of the activity definition to retrieve.</param>
        public GetActivityDefinitionByIdQuery(Guid activityDefinitionId)
        {
            ActivityDefinitionId = activityDefinitionId;
        }
    }
}
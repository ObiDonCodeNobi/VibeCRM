using MediatR;
using VibeCRM.Application.Features.ActivityDefinition.DTOs;

namespace VibeCRM.Application.Features.ActivityDefinition.Queries.GetAllActivityDefinitions
{
    /// <summary>
    /// Query for retrieving all active activity definitions from the system.
    /// Implements the CQRS query pattern for retrieving multiple entities.
    /// </summary>
    public class GetAllActivityDefinitionsQuery : IRequest<List<ActivityDefinitionListDto>>
    {
        /// <summary>
        /// Gets or sets the page number for pagination (1-based)
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Gets or sets the page size for pagination
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllActivityDefinitionsQuery"/> class.
        /// </summary>
        public GetAllActivityDefinitionsQuery()
        {
        }
    }
}
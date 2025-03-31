using MediatR;
using VibeCRM.Shared.DTOs.PersonStatus;

namespace VibeCRM.Application.Features.PersonStatus.Queries.GetPersonStatusByOrdinalPosition
{
    /// <summary>
    /// Query for retrieving person statuses ordered by their ordinal position.
    /// Implements the CQRS query pattern for retrieving person status entities in a specific order.
    /// </summary>
    public class GetPersonStatusByOrdinalPositionQuery : IRequest<IEnumerable<PersonStatusListDto>>
    {
        /// <summary>
        /// Gets or sets a value indicating whether to include inactive (soft-deleted) person statuses.
        /// </summary>
        public bool IncludeInactive { get; set; } = false;
    }
}
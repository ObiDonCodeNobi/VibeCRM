using MediatR;
using VibeCRM.Application.Features.PersonType.DTOs;

namespace VibeCRM.Application.Features.PersonType.Queries.GetPersonTypeByOrdinalPosition
{
    /// <summary>
    /// Query to retrieve person types ordered by their ordinal position.
    /// Implements the CQRS query pattern for retrieving ordered person type entities.
    /// </summary>
    public class GetPersonTypeByOrdinalPositionQuery : IRequest<IEnumerable<PersonTypeListDto>>
    {
        // No parameters needed for this query as it retrieves all person types ordered by ordinal position
    }
}
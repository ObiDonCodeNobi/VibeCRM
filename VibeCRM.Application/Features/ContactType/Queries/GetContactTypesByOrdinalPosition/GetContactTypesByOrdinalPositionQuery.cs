using MediatR;
using VibeCRM.Shared.DTOs.ContactType;

namespace VibeCRM.Application.Features.ContactType.Queries.GetContactTypesByOrdinalPosition
{
    /// <summary>
    /// Query to retrieve contact types ordered by their ordinal position.
    /// </summary>
    public class GetContactTypesByOrdinalPositionQuery : IRequest<IEnumerable<ContactTypeListDto>>
    {
        // No parameters needed for this query
    }
}
using MediatR;
using VibeCRM.Application.Features.PersonType.DTOs;

namespace VibeCRM.Application.Features.PersonType.Queries.GetAllPersonTypes
{
    /// <summary>
    /// Query to retrieve all active person types.
    /// Implements the CQRS query pattern for retrieving person type entities.
    /// </summary>
    public class GetAllPersonTypesQuery : IRequest<IEnumerable<PersonTypeListDto>>
    {
        // No parameters needed for this query as it retrieves all active person types
    }
}

using MediatR;
using VibeCRM.Application.Features.ContactType.DTOs;

namespace VibeCRM.Application.Features.ContactType.Queries.GetAllContactTypes
{
    /// <summary>
    /// Query to retrieve all contact types.
    /// </summary>
    public class GetAllContactTypesQuery : IRequest<IEnumerable<ContactTypeListDto>>
    {
        // No parameters needed for this query
    }
}
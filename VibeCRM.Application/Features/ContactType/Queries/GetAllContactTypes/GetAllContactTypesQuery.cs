using MediatR;
using VibeCRM.Shared.DTOs.ContactType;

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
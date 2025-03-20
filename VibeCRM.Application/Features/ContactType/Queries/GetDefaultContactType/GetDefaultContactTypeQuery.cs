using MediatR;
using VibeCRM.Application.Features.ContactType.DTOs;

namespace VibeCRM.Application.Features.ContactType.Queries.GetDefaultContactType
{
    /// <summary>
    /// Query to retrieve the default contact type.
    /// </summary>
    public class GetDefaultContactTypeQuery : IRequest<ContactTypeDto>
    {
        // No parameters needed for this query
    }
}
using MediatR;
using VibeCRM.Shared.DTOs.ContactType;

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
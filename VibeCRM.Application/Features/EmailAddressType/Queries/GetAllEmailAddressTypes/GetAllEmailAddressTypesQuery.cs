using MediatR;
using VibeCRM.Application.Features.EmailAddressType.DTOs;

namespace VibeCRM.Application.Features.EmailAddressType.Queries.GetAllEmailAddressTypes
{
    /// <summary>
    /// Query to retrieve all email address types.
    /// </summary>
    public class GetAllEmailAddressTypesQuery : IRequest<IEnumerable<EmailAddressTypeListDto>>
    {
        // No parameters needed for this query
    }
}
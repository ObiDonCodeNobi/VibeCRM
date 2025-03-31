using MediatR;
using VibeCRM.Shared.DTOs.EmailAddressType;

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
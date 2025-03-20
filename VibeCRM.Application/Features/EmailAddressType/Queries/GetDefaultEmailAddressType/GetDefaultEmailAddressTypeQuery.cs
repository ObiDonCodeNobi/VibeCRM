using MediatR;
using VibeCRM.Application.Features.EmailAddressType.DTOs;

namespace VibeCRM.Application.Features.EmailAddressType.Queries.GetDefaultEmailAddressType
{
    /// <summary>
    /// Query to retrieve the default email address type (typically the one with the lowest ordinal position).
    /// </summary>
    public class GetDefaultEmailAddressTypeQuery : IRequest<EmailAddressTypeDto>
    {
        // No parameters needed for this query
    }
}
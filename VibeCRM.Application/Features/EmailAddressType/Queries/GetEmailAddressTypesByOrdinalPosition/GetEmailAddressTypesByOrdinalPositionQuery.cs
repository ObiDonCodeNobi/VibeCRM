using MediatR;
using VibeCRM.Application.Features.EmailAddressType.DTOs;

namespace VibeCRM.Application.Features.EmailAddressType.Queries.GetEmailAddressTypesByOrdinalPosition
{
    /// <summary>
    /// Query to retrieve email address types ordered by their ordinal position.
    /// </summary>
    public class GetEmailAddressTypesByOrdinalPositionQuery : IRequest<IEnumerable<EmailAddressTypeDto>>
    {
        // No parameters needed for this query
    }
}
using MediatR;
using VibeCRM.Shared.DTOs.EmailAddressType;

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
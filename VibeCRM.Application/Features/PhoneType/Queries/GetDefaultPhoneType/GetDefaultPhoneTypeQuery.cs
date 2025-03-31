using MediatR;
using VibeCRM.Shared.DTOs.PhoneType;

namespace VibeCRM.Application.Features.PhoneType.Queries.GetDefaultPhoneType
{
    /// <summary>
    /// Query to retrieve the default phone type.
    /// The default phone type is typically the one with the lowest ordinal position.
    /// </summary>
    public class GetDefaultPhoneTypeQuery : IRequest<PhoneTypeDto>
    {
        // No parameters needed for this query
    }
}
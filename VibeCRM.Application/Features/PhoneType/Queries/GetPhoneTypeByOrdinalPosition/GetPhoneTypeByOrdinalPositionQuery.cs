using MediatR;
using VibeCRM.Application.Features.PhoneType.DTOs;

namespace VibeCRM.Application.Features.PhoneType.Queries.GetPhoneTypeByOrdinalPosition
{
    /// <summary>
    /// Query to retrieve a phone type by its ordinal position.
    /// </summary>
    public class GetPhoneTypeByOrdinalPositionQuery : IRequest<PhoneTypeDto>
    {
        /// <summary>
        /// Gets or sets the ordinal position of the phone type to retrieve.
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}
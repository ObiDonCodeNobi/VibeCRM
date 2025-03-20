using MediatR;
using VibeCRM.Application.Features.PhoneType.DTOs;

namespace VibeCRM.Application.Features.PhoneType.Queries.GetPhoneTypeByType
{
    /// <summary>
    /// Query to retrieve a phone type by its type name.
    /// </summary>
    public class GetPhoneTypeByTypeQuery : IRequest<PhoneTypeDto>
    {
        /// <summary>
        /// Gets or sets the type name of the phone type to retrieve.
        /// </summary>
        public string Type { get; set; } = string.Empty;
    }
}
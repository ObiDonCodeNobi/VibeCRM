using MediatR;
using VibeCRM.Application.Features.Phone.DTOs;

namespace VibeCRM.Application.Features.Phone.Queries.GetPhonesByPhoneType
{
    /// <summary>
    /// Query to retrieve all phones of a specific phone type
    /// </summary>
    public class GetPhonesByPhoneTypeQuery : IRequest<IEnumerable<PhoneListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the phone type
        /// </summary>
        public Guid PhoneTypeId { get; set; }
    }
}
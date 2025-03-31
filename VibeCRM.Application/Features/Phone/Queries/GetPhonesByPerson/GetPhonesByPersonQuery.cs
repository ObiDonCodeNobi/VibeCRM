using MediatR;
using VibeCRM.Shared.DTOs.Phone;

namespace VibeCRM.Application.Features.Phone.Queries.GetPhonesByPerson
{
    /// <summary>
    /// Query to retrieve all phones associated with a specific person
    /// </summary>
    public class GetPhonesByPersonQuery : IRequest<IEnumerable<PhoneListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the person
        /// </summary>
        public Guid PersonId { get; set; }
    }
}
using MediatR;
using VibeCRM.Shared.DTOs.Phone;

namespace VibeCRM.Application.Features.Phone.Queries.GetPhonesByCompany
{
    /// <summary>
    /// Query to retrieve all phones associated with a specific company
    /// </summary>
    public class GetPhonesByCompanyQuery : IRequest<IEnumerable<PhoneListDto>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the company
        /// </summary>
        public Guid CompanyId { get; set; }
    }
}
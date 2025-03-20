using MediatR;

namespace VibeCRM.Application.Features.Phone.Commands.RemovePhoneFromCompany
{
    /// <summary>
    /// Command for removing an association between a phone and a company
    /// </summary>
    public class RemovePhoneFromCompanyCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the phone
        /// </summary>
        public Guid PhoneId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the company
        /// </summary>
        public Guid CompanyId { get; set; }
    }
}
using MediatR;

namespace VibeCRM.Application.Features.Phone.Commands.AddPhoneToCompany
{
    /// <summary>
    /// Command for associating a phone with a company
    /// </summary>
    public class AddPhoneToCompanyCommand : IRequest<bool>
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
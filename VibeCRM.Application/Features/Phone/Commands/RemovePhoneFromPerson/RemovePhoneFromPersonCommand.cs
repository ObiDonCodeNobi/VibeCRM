using MediatR;

namespace VibeCRM.Application.Features.Phone.Commands.RemovePhoneFromPerson
{
    /// <summary>
    /// Command for removing an association between a phone and a person
    /// </summary>
    public class RemovePhoneFromPersonCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the phone
        /// </summary>
        public Guid PhoneId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the person
        /// </summary>
        public Guid PersonId { get; set; }
    }
}
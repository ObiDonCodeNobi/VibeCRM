using MediatR;

namespace VibeCRM.Application.Features.Phone.Commands.AddPhoneToPerson
{
    /// <summary>
    /// Command for associating a phone with a person
    /// </summary>
    public class AddPhoneToPersonCommand : IRequest<bool>
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
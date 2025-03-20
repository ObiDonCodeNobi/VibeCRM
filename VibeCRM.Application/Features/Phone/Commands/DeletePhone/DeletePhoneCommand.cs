using MediatR;

namespace VibeCRM.Application.Features.Phone.Commands.DeletePhone
{
    /// <summary>
    /// Command for deleting a phone record
    /// </summary>
    public class DeletePhoneCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the phone to delete
        /// </summary>
        public Guid Id { get; set; }
    }
}
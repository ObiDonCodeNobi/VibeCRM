using MediatR;

namespace VibeCRM.Application.Features.PhoneType.Commands.DeletePhoneType
{
    /// <summary>
    /// Command for deleting an existing phone type.
    /// </summary>
    public class DeletePhoneTypeCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the phone type to delete.
        /// </summary>
        public Guid Id { get; set; }
    }
}

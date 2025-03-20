using MediatR;

namespace VibeCRM.Application.Features.EmailAddressType.Commands.DeleteEmailAddressType
{
    /// <summary>
    /// Command to delete (soft delete) an email address type.
    /// </summary>
    public class DeleteEmailAddressTypeCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the email address type to delete.
        /// </summary>
        public Guid Id { get; set; }
    }
}
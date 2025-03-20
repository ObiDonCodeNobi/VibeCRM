using MediatR;

namespace VibeCRM.Application.Features.EmailAddress.Commands.DeleteEmailAddress
{
    /// <summary>
    /// Command for soft-deleting an existing email address in the system.
    /// Implements IRequest to return a boolean indicating success or failure.
    /// </summary>
    public class DeleteEmailAddressCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the email address to delete.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who is deleting the email address.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the email address is being deleted.
        /// </summary>
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
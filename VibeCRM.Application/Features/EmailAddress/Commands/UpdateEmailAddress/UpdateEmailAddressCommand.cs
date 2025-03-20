using MediatR;

namespace VibeCRM.Application.Features.EmailAddress.Commands.UpdateEmailAddress
{
    /// <summary>
    /// Command for updating an existing email address in the system.
    /// Implements IRequest to return a boolean indicating success or failure.
    /// </summary>
    public class UpdateEmailAddressCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the email address to update.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the email address type identifier.
        /// </summary>
        public Guid EmailAddressTypeId { get; set; }

        /// <summary>
        /// Gets or sets the email address string.
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ID of the user who is modifying the email address.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the email address is being modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets whether the email address is active.
        /// </summary>
        public bool Active { get; set; } = true;
    }
}
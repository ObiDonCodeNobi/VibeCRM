using MediatR;

namespace VibeCRM.Application.Features.EmailAddress.Commands.CreateEmailAddress
{
    /// <summary>
    /// Command for creating a new email address in the system.
    /// Implements IRequest to return the ID of the created email address.
    /// </summary>
    public class CreateEmailAddressCommand : IRequest<Guid>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the email address.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the email address type identifier.
        /// </summary>
        public Guid EmailAddressTypeId { get; set; }

        /// <summary>
        /// Gets or sets the email address string.
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ID of the user who is creating the email address.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the email address is being created.
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the ID of the user who is initially modifying the email address.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the email address is initially modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
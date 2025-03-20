using MediatR;

namespace VibeCRM.Application.Features.EmailAddressType.Commands.CreateEmailAddressType
{
    /// <summary>
    /// Command to create a new email address type.
    /// </summary>
    public class CreateEmailAddressTypeCommand : IRequest<Guid>
    {
        /// <summary>
        /// Gets or sets the email address type name (e.g., "Personal", "Work").
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address type description with details about when this email address type should be used.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting email address types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}
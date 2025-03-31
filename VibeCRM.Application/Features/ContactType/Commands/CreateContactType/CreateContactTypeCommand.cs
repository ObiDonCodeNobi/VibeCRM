using MediatR;
using VibeCRM.Shared.DTOs.ContactType;

namespace VibeCRM.Application.Features.ContactType.Commands.CreateContactType
{
    /// <summary>
    /// Command for creating a new contact type.
    /// </summary>
    public class CreateContactTypeCommand : IRequest<ContactTypeDto>
    {
        /// <summary>
        /// Gets or sets the contact type name (e.g., "Customer", "Vendor", "Partner").
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the contact type description with details about when this contact type should be used.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting contact types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}
using MediatR;
using VibeCRM.Application.Features.PhoneType.DTOs;

namespace VibeCRM.Application.Features.PhoneType.Commands.UpdatePhoneType
{
    /// <summary>
    /// Command for updating an existing phone type.
    /// </summary>
    public class UpdatePhoneTypeCommand : IRequest<PhoneTypeDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the phone type to update.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the phone type name (e.g., "Home", "Work", "Mobile", "Fax").
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone type description with details about when this phone type should be used.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting phone types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}
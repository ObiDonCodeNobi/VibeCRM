using MediatR;
using VibeCRM.Shared.DTOs.PhoneType;

namespace VibeCRM.Application.Features.PhoneType.Commands.CreatePhoneType
{
    /// <summary>
    /// Command for creating a new phone type.
    /// </summary>
    public class CreatePhoneTypeCommand : IRequest<PhoneTypeDto>
    {
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
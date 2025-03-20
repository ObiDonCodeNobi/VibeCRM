using MediatR;
using VibeCRM.Application.Features.AddressType.DTOs;

namespace VibeCRM.Application.Features.AddressType.Commands.UpdateAddressType
{
    /// <summary>
    /// Command for updating an existing address type.
    /// </summary>
    public class UpdateAddressTypeCommand : IRequest<AddressTypeDto>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the address type to update.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the address type name (e.g., "Home", "Work", "Billing", "Shipping").
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the address type description with details about when this address type should be used.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ordinal position for sorting address types in listings and dropdowns.
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}
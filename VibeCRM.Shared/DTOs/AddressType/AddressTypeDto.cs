namespace VibeCRM.Shared.DTOs.AddressType
{
    /// <summary>
    /// Data Transfer Object for address type information.
    /// Contains the basic properties of an address type.
    /// </summary>
    public class AddressTypeDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the address type.
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
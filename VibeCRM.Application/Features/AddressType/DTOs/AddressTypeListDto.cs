namespace VibeCRM.Application.Features.AddressType.DTOs
{
    /// <summary>
    /// Data Transfer Object for address type list information.
    /// Contains the basic properties of an address type along with address count.
    /// Used for displaying address types in list views.
    /// </summary>
    public class AddressTypeListDto
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

        /// <summary>
        /// Gets or sets the number of addresses associated with this address type.
        /// </summary>
        public int AddressCount { get; set; }
    }
}
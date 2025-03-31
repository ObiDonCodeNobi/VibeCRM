namespace VibeCRM.Shared.DTOs.AddressType
{
    /// <summary>
    /// Data Transfer Object for detailed address type information.
    /// Contains the basic properties of an address type along with audit information and address count.
    /// Used for displaying detailed address type information.
    /// </summary>
    public class AddressTypeDetailsDto
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

        /// <summary>
        /// Gets or sets the identifier of the user who created this address type.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when this address type was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified this address type.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when this address type was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this address type is active.
        /// </summary>
        public bool Active { get; set; }
    }
}
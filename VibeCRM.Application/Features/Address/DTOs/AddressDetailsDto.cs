namespace VibeCRM.Application.Features.Address.DTOs
{
    /// <summary>
    /// Detailed Data Transfer Object for Address entities.
    /// Contains all properties needed for detailed views and operations.
    /// </summary>
    public class AddressDetailsDto : AddressDto
    {
        /// <summary>
        /// Gets or sets the name of the address type.
        /// </summary>
        public string AddressTypeName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the state or province.
        /// </summary>
        public string StateName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the full address as a formatted string.
        /// </summary>
        public string FullAddress { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the identifier of the user who created this address.
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when this address was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who last modified this address.
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time when this address was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}
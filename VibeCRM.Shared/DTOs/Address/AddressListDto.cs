namespace VibeCRM.Shared.DTOs.Address
{
    /// <summary>
    /// List Data Transfer Object for Address entities.
    /// Contains properties needed for displaying addresses in lists.
    /// </summary>
    public class AddressListDto : AddressDto
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
    }
}